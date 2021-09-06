using System;
using System.Collections.Generic;

namespace weBelieveIT.resultModels
{
    public class TeamOtd
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<TeamMemberOtd> Member { get; set; }
    }

    public class TeamMemberOtd
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}