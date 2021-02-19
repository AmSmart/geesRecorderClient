using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    public record ServerStateDTO
    {
        public bool LoggedIn { get; set; }

        public string AccessToken { get; set; }

        public string Pin { get; set; }
    }
}
