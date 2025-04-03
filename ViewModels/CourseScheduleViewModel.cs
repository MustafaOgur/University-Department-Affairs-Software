using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UDAS.Entities;

namespace UDAS.ViewModels
{
    public class CourseScheduleViewModel
    {
        public List<Course>? Courses { get; set; }
        public List<Classroom>? Classrooms { get; set; }
        public List<CourseTime>? CourseTimes { get; set; }
        public List<User>? Users { get; set; }
        public List<WeekDay>? WeekDays { get; set; }

    }
}