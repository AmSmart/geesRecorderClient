using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.Models
{

    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ProjectType Type { get; set; }
    }

    public enum ProjectType
    {
        None,
        Attendance,
        DataCollection
    }
}
