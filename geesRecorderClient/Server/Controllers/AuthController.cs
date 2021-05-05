﻿using geesRecorderClient.Server.Data;
using geesRecorderClient.Shared;
using geesRecorderClient.Shared.DTOs;
using geesRecorderClient.Shared.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace geesRecorderClient.Server.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public AuthController(IWebHostEnvironment webHostEnvironment,
            IHttpClientFactory httpClientFactory,
            ApplicationDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(ApiRoutes.ApiBaseAddress);

            if (_dbContext.ServerState.FirstOrDefault() is null)
            {
                _dbContext.ServerState.Add(new ServerState
                {
                    AccessToken = "",
                    LoggedIn = false,
                    Pin = "1234", // TODO: Remove default pin later
                    LockedRoute = "",
                    RouteLockActivated = false
                });
                _dbContext.SaveChanges();
            }
        }

        [HttpGet("state")]
        public IActionResult GetState()
        {
            var state = _dbContext.ServerState.First();
            return Ok(state);
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.ApiSignUp, dto);
            if (response.IsSuccessStatusCode)
            {
                var token = JsonSerializer.Deserialize<JwtTokenDTO>(await response.Content.ReadAsStringAsync(), _jsonOptions);
                await UpdateServerStateAsync(accessToken: token.Token, loggedIn: true);
                return Ok();
            }

            return BadRequest(await response.Content.ReadAsStringAsync());
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.ApiSignIn, dto);
            if (response.IsSuccessStatusCode)
            {
                var token = JsonSerializer.Deserialize<JwtTokenDTO>(await response.Content.ReadAsStringAsync(), _jsonOptions);
                await UpdateServerStateAsync(accessToken: token.Token, loggedIn: true);
                return Ok();
            }
            return Unauthorized(await response.Content.ReadAsStringAsync());
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh()
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.ApiRefreshToken, new { });
            if (response.IsSuccessStatusCode)
            {
                var token = JsonSerializer.Deserialize<JwtTokenDTO>(await response.Content.ReadAsStringAsync(), _jsonOptions);
                await UpdateServerStateAsync(accessToken: token.Token, loggedIn: true);
                return Ok();
            }
            await UpdateServerStateAsync(accessToken: "", loggedIn: false);
            return Unauthorized(await response.Content.ReadAsStringAsync());
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var projects = await _dbContext.Projects.ToListAsync();
            var dashboardDTO = new DashboardDTO
            {
                UserName = "Smart",
                Projects = projects
            };

            return Ok(dashboardDTO);
        }

        [HttpPost("create-project")]
        public async Task<IActionResult> CreateProject(Project project)
        {
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-project")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _dbContext.Projects.FindAsync(id);
            
            if(project.Type == ProjectType.Attendance)
            {
                project = await _dbContext.AttendanceProjects.FindAsync(id);
            }
            else if(project.Type == ProjectType.DataCollection)
            {
                project = await _dbContext.DataCollectionProjects.FindAsync(id);
            }
            return Ok(project);
        }

        [HttpDelete("del-project")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _dbContext.Projects.FindAsync(id);
            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("lock-route")]
        public async Task<IActionResult> LockRoute(string route)
        {
            await UpdateServerStateAsync(routeLockActivated: true, lockedRoute: route);
            return Ok();

        }

        [HttpPatch("unlock-route")]
        public async Task<IActionResult> UnlockRoute()
        {
            await UpdateServerStateAsync(routeLockActivated: false, lockedRoute: "");
            return Ok();
        }

        private async Task UpdateServerStateAsync(string accessToken = null, bool? loggedIn = null, string pin = null,
            bool? routeLockActivated = null, string lockedRoute = null)
        {
            var serverState = _dbContext.ServerState.First();
            serverState.AccessToken = accessToken ?? serverState.AccessToken;
            serverState.LoggedIn = loggedIn ?? serverState.LoggedIn;
            serverState.Pin = pin ?? serverState.Pin;
            serverState.RouteLockActivated = routeLockActivated ?? serverState.RouteLockActivated;
            serverState.LockedRoute = lockedRoute ?? lockedRoute;
            await _dbContext.SaveChangesAsync();
        }
    }
}