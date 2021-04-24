using geesRecorderClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    public record EnrolPersonDTO
    {
        public Person Person { get; set; }

        public int ProjectId { get; set; }

        public int FingerPrintId { get; set; }
    }
}
