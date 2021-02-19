using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    public record SignInDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
