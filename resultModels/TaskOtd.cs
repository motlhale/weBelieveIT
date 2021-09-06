using System;
using System.Collections.Generic;

namespace weBelieveIT.resultModels
{
    public class TaskOtd
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set;}
        public Priority Priority { get; set; }
        public TaskMemberOtd Member { get; set; }
    }

    public class TaskMemberOtd
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}