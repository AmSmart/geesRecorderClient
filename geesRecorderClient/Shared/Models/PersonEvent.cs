using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.Models
{
    public class PersonEvent
    {
        public int Id { get; set; }

        public Event Event { get; set; }

        public Person Person { get; set; }

        public DateTime TimeIn { get; set; }

        public DateTime TimeOut { get; set; }
    }
}