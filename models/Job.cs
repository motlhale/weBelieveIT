using System;
using System.Collections.Generic;

namespace weBelieveIT.models
{
    public class Job
    {
        public string JobNumber { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public List<Task> Tasks { get; set; }
    }
}