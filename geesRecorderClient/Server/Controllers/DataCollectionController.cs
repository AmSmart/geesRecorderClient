using geesRecorderClient.Server.Data;
using geesRecorderClient.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Server.Controllers
{
    [ApiController]
    [Route("datacol")]
    public class DataCollectionController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public DataCollectionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }       
        
        [HttpPost("questions")]
        public async Task<IActionResult> UpdateQuestions(int projectId, [FromBody] List<Question> questions)
        {
            var dataCollectionProject = await _dbContext.DataCollectionProjects.FindAsync(projectId);
            dataCollectionProject.Questions = questions;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("response")]
        public async Task<IActionResult> UploadResponse(int projectId, [FromBody] List<string> answers)
        {
            var dataCollectionProject = await _dbContext.DataCollectionProjects.FindAsync(projectId);
            dataCollectionProject.Responses.Add(new Response
            {
                Answers = answers
            });
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("publish-survey")]
        public async Task<IActionResult> PublishDataCollectionSurvey(int projectId)
        {
            var dataCollectionProject = await _dbContext.DataCollectionProjects.FindAsync(projectId);
            dataCollectionProject.Published = true;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        
        [HttpPost("create-with-questions")]
        public async Task<IActionResult> PublishDataCollectionSurvey(string projectName, List<Question> questions)
        {
            var project = new DataCollectionProject
            {
                Name = projectName,
                Questions = questions,
                Type = ProjectType.DataCollection
            };

            _dbContext.DataCollectionProjects.Add(project);
            await _dbContext.SaveChangesAsync();
            return Ok(project);
        }
    }
}