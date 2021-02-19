using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Server.Models
{
    public class Fingerprint
    {
        public int Id { get; set; }
        
        public int AttendantId { get; set; }

        public int SensorId { get; set; }
    }
}
