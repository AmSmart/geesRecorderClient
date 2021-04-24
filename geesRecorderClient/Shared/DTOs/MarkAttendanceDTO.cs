using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.DTOs
{
    public class MarkAttendanceDTO
    {
        public int PersonId { get; set; }

        public int EventId { get; set; }

        public DateTime AttendanceTimeStamp { get; set; }
    }
}
