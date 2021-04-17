using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.Models
{
    public class AttendanceProject : Project
    {
        public List<Person> Persons { get; set; }

        public List<Event> Events { get; set; }

        public List<PersonEvent> PersonEvents { get; set; }
    }
}
