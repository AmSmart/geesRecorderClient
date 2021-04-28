using geesRecorderClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    public record EnrolPersonDTO
    {
        [Required]        
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string CustomId { get; set; }        

        public int ProjectId { get; set; }

        public int FingerPrintId { get; set; }
    }
}
