using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Server.Services
{
    public class FingerprintCommsHub : Hub
    {
        private readonly FingerprintRunner _fingerprintRunner;

        public FingerprintCommsHub(FingerprintRunner fingerprintRunner)
        {
            _fingerprintRunner = fingerprintRunner;
        }

        public override Task OnConnectedAsync()
        {
            _fingerprintRunner.Enrol();
            return base.OnConnectedAsync();
        }
    }
}
