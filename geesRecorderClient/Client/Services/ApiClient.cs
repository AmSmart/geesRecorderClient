using geesRecorderClient.Shared.DTOs;
using geesRecorderClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace geesRecorderClient.Client.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<OperationResult> CreateProject(string name, ProjectType type)
        {
            var project = new Project
            {
                Name = name,
                Type = type
            };

            var result = await _httpClient.PostAsJsonAsync("auth/create-project", project);
            
            if (result.IsSuccessStatusCode)
                return new OperationResult(true);

            return new OperationResult(await result.Content.ReadAsStringAsync());
        }

        public async Task<OperationResult> EnrolPerson(EnrolPersonDTO dto)
        {
            var result = await _httpClient.PostAsJsonAsync("attendance/enrol", dto);

            if (result.IsSuccessStatusCode)
                return new OperationResult(true);

            return new OperationResult(await result.Content.ReadAsStringAsync());
        }
        
        public async Task<OperationResult> AddNewEvent(EnrolPersonDTO dto)
        {
            var result = await _httpClient.PostAsJsonAsync("attendance/event", dto);

            if (result.IsSuccessStatusCode)
                return new OperationResult(true);

            return new OperationResult(await result.Content.ReadAsStringAsync());
        }
        
        public async Task<OperationResult> MarkAttendanceIn(MarkAttendanceDTO dto)
        {
            var result = await _httpClient.PostAsJsonAsync("attendance/mark-in", dto);

            if (result.IsSuccessStatusCode)
                return new OperationResult(true);

            return new OperationResult(await result.Content.ReadAsStringAsync());
        }
        
        public async Task<OperationResult> MarkAttendanceOut(MarkAttendanceDTO dto)
        {
            var result = await _httpClient.PostAsJsonAsync("attendance/mark-out", dto);

            if (result.IsSuccessStatusCode)
                return new OperationResult(true);

            return new OperationResult(await result.Content.ReadAsStringAsync());
        }

        public async Task<OperationDataResult<DashboardDTO>> GetDashboard()
        {
            var result = await _httpClient.GetAsync("auth/dashboard");
            string resultContent = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var dashboard = JsonSerializer.Deserialize<DashboardDTO>(resultContent, _jsonOptions);
                return new OperationDataResult<DashboardDTO>(dashboard);
            }
            return new OperationDataResult<DashboardDTO>(resultContent);
        }

        public async Task<OperationDataResult<Project>> GetProject(int id)
        {
            var result = await _httpClient.GetAsync($"auth/get-project?id={id}");
            string resultContent = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                Project project = JsonSerializer.Deserialize<Project>(resultContent, _jsonOptions);
                switch (project.Type)
                {
                    case ProjectType.Attendance:
                        project = JsonSerializer.Deserialize<AttendanceProject>(resultContent, _jsonOptions);
                        break;

                    case ProjectType.DataCollection:
                        project = JsonSerializer.Deserialize<DataCollectionProject>(resultContent, _jsonOptions);
                        break;
                }

                return new OperationDataResult<Project>(project);
            }
            return new OperationDataResult<Project>(error: resultContent);
        }
        
        public async Task<OperationResult> DeleteProject(int id)
        {
            var result = await _httpClient.DeleteAsync($"auth/del-project?id={id}");
            string resultContent = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                return new OperationResult(true);
            }
            return new OperationResult(error: resultContent);
        }

        public async Task<OperationResult> UpdateDataCollectionQuestions(int projectId, List<Question> questions)
        {
            var result = await _httpClient.PostAsJsonAsync($"datacol/questions?projectId={projectId}", questions);
            string resultContent = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                return new OperationResult(true);
            }

            return new OperationResult(resultContent);
        }
        
        public async Task<OperationResult> UploadDataCollectionResponse(int projectId, List<string> answers)
        {
            var result = await _httpClient.PostAsJsonAsync($"datacol/response?projectId={projectId}", answers);
            string resultContent = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                return new OperationResult(true);
            }

            return new OperationResult(resultContent);
        }
        
        public async Task<OperationResult> PublishDataColSurvey(int projectId)
        {
            var result = await _httpClient.PostAsJsonAsync($"datacol/publish-survey?projectId={projectId}", "");
            string resultContent = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                return new OperationResult(true);
            }

            return new OperationResult(resultContent);
        }

        public async Task<OperationDataResult<DataCollectionProject>> CreateDataColProjectWithQuestions(string projectName, List<Question> questions)
        {
            var result = await _httpClient.PostAsJsonAsync($"datacol/create-with-questions?projectName={projectName}", questions);
            string resultContent = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var project = JsonSerializer.Deserialize<DataCollectionProject>(resultContent, _jsonOptions);
                return new OperationDataResult<DataCollectionProject>(project);
            }

            return new OperationDataResult<DataCollectionProject>(resultContent);
        }
    }
}