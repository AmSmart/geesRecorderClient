using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace geesRecorderClient.Shared.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public List<string> Options { get; set; }

        public QuestionType Type { get; set; }

        [JsonIgnore]
        public virtual DataCollectionProject DataCollectionProject { get; set; }
    }

    public enum QuestionType
    {
        SingleChoice,
        MultiChoice,
        Text
    }
}
