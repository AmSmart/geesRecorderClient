using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.Models
{
    public class DataCollectionProject : Project
    {
        public virtual List<Question> Questions { get; set; }

        public virtual List<Response> Responses { get; set; }

        public bool Published { get; set; }
    }
}