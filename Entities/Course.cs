using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UDAS.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string? CourseName { get; set; }
        public string? Semester { get; set; }
    }
}