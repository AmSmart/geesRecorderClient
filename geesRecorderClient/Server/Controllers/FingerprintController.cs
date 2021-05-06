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

        [HttpPost("enrol/id")]
        public async Task<IActionResult> EnrolFingerprint(int id, bool exists)
        {            
            await _hubContext.Clients.All.SendAsync("id", id, exists);
            return Ok();
        }
        
        [HttpPost("search/id")]
        public async Task<IActionResult> SearchFingerprint(int id)
        {            
            await _hubContext.Clients.All.SendAsync("id", id);
            return Ok();
        }
    }
}
