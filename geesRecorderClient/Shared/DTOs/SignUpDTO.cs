using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    public record SignUpDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string Pin { get; set; }
    }
}
