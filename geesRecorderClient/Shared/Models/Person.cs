using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.Models
{
    public class Person
    {
        public int Id { get; set; }

        // TODO: 
        public List<int> FingerprintIds { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        // TODO: Annotate as key
        public string CustomId { get; set; }

        [JsonIgnore]
        public virtual List<AttendanceProject> AttendanceProjects { get; set; }
    }
}

// TODO: Model details like Firstname, Lastname and CustomId should be editable
