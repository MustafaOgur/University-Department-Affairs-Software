using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UDAS.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }

        public string? Day { get; set; }
        public string? Classroom { get; set; }

        public string? Lecturer { get; set; }

        public int scheduleNumber { get; set; }

    }
}