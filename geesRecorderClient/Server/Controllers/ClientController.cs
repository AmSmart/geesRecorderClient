using geesRecorderClient.Shared.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace geesRecorderClient.Server.Controllers
{
    [ApiController]
    [Route("client")]
    public class ClientController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _statePath;
        private readonly HttpClient _httpClient;

        public ClientController(IWebHostEnvironment webHostEnvironment, IHttpClientFactory httpClientFactory)
        {
            _webHostEnvironment = webHostEnvironment;
            _statePath = Path.Combine(_webHostEnvironment.ContentRootPath, "state.json");
            _httpClient = httpClientFactory.CreateClient();

            if(!System.IO.File.Exists(_statePath))
            {
                var file = System.IO.File.Create(_statePath);
                file.Close();

                System.IO.File.WriteAllText(_statePath, JsonSerializer.Serialize(new ServerStateDTO
                {
                    AccessToken = "",
                    LoggedIn = false,
                    Pin = ""
                }));
            }
        }

        [HttpGet("state")]
        public IActionResult GetState()
            => Ok(JsonSerializer.Deserialize<ServerStateDTO>(System.IO.File.ReadAllText(_statePath)));

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO dto)
        {
            return Ok("");
        }


    }
}
