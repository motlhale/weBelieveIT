using System;
using System.Collections.Generic;

namespace weBelieveIT.resultModels
{
    public class JobOtd
    {
        public string JobNumber { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public List<JobTaskOtd> Tasks { get; set; }
    }

    public class JobTaskOtd
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set;}
        public Priority Priority { get; set; }
    }

}