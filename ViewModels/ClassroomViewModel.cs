using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UDAS.Entities;

namespace UDAS.ViewModels
{
    public class ClassroomViewModel
    {   
        public Classroom? Classroom { get; set; }
        public List<Schedule>? Schedules { get; set; }
        public List<CourseTime>? CourseTimes { get; set; }
        public List<User>? Lecturers { get; set; }
        public List<string>? Days { get; set; }

    }
}