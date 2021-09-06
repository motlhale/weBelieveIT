using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace weBelieveIT.models
{
    public class Member
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public Team Team { get; set; }
        public List<Task> Tasks { get; set; }
    }
}