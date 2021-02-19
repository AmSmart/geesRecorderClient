using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Server.Models
{
    public class AttendantEvent
    {
        public int EventId { get; set; }

        public int AttendantId { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
