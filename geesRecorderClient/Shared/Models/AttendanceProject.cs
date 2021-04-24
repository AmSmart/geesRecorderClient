using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.Models
{
    public class AttendanceProject : Project
    {
        public virtual List<Person> Persons { get; set; }

        public virtual List<Event> Events { get; set; }
    }
}
