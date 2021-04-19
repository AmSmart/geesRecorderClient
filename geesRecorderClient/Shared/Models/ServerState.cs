using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.Models
{
    public record ServerState
    {
        public int Id { get; set; }

        public bool LoggedIn { get; set; }

        public string AccessToken { get; set; }

        public string Pin { get; set; }

        public bool RouteLockActivated { get; set; }

        public string LockedRoute { get; set; }
    }
}
