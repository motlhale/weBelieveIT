using System;
using System.Text.Json.Serialization;

namespace weBelieveIT.models
{
    public class Task
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set;}
        public Priority Priority { get; set; }
        [JsonIgnore]
        public Member Member { get; set; }
        [JsonIgnore]
        public Job Job { get; set; }
    }
}