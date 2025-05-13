using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UDAS.Entities;

namespace UDAS.ViewModels
{
    public class ExamAddViewModel
    {   
        public List<CourseTime>? CourseTimes { get; set; }
        public List<Course>? Courses { get; set; }
        public List<Classroom>? Classrooms { get; set; }
        public List<User>? Supervisors { get; set; }

    }
}