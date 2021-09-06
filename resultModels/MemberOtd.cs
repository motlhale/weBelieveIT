using System;
using System.Collections.Generic;

namespace weBelieveIT.resultModels
{
    public class MemberOtd
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public MemberTeamOtd Team { get; set; } 
    }

    public class MemberTeamOtd
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}