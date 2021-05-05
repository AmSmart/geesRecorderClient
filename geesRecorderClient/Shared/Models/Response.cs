using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.Models
{
    public class Response
    {
        public int Id { get; set; }

        public List<string> Answers { get; set; }

        [JsonIgnore]
        public virtual DataCollectionProject DataCollectionProject { get; set; }
    }
}