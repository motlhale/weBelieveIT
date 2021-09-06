using System;

namespace weBelieveIT.Presentation
{
    public class TaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set;}
        public Priority Priority { get; set; }
    }
}