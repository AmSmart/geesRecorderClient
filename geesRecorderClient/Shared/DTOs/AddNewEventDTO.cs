using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    public record AddNewEventDTO
    {
        public int ProjectId { get; set; }

        public string EventName { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
