using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    public record EventDetailsDTO
    {
        public string EventName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TotalAttendeesCount { get; set; }

        public DateTime? MeanArrivalTime { get; set; }

        public DateTime? MeanDepartureTime { get; set; }
    }
}
