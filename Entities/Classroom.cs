using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UDAS.Entities
{
    public class Classroom
    {
        public int Id { get; set; }
        public string? RoomName { get; set; }
        public string? Capacity { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<ExamSchedule> ExamSchedules { get; set; }
    }
}