using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UDAS.Entities
{
    public class Schedule
    {
        public int Id { get; set; } 

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public string? Day { get; set; }
        
        public int? ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }


        public int? CourseId { get; set; }
        public Course? Course { get; set; }


        public int? LecturerId { get; set; }
        public User? Lecturer { get; set; }

        [Required]
        public string? scheduleName { get; set; }

        public string? status { get; set; } 

    }
}