using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace weBelieveIT.models
{
    public class Team
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Member> Members { get; set; }
    }
}