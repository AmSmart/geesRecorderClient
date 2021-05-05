using geesRecorderClient.Server.Services;
using geesRecorderClient.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Server.Controllers
{
    [ApiController]
    [Route("fingerprint")]
    public class FingerprintController : ControllerBase
    {
        private readonly IHubContext<FingerprintCommsHub> _hubContext;        

        public FingerprintController(IHubContext<FingerprintCommsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("state")]
        public async Task<IActionResult> State(int statusCode)
        {
            if(statusCode >= FingerprintConstants.TouchSensor1 && statusCode <= FingerprintConstants.Error)
            {
                await _hubContext.Clients.All.SendAsync("state", statusCode, FingerprintConstants.StatusMessages[statusCode]);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("id")]
        public async Task<IActionResult> FingerprintId([FromBody] int id)
        {
            await _hubContext.Clients.All.SendAsync("id", id);
            return Ok();
        }
    }
}
