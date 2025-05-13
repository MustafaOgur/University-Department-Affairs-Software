using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UDAS.Entities
{
    public class ExamSchedule
    {
        public int Id { get; set; } 
        
        public string? Date { get; set; } 
        public string? StartTime { get; set; } 
        public string? EndTime { get; set; } 

        public int? CourseId { get; set; } 
        public Course? Course { get; set; } 

        public int? ClassroomId { get; set; } 
        public Classroom? Classroom { get; set; } 

        public int? SupervisorId { get; set; } 
        public User? Supervisor { get; set; } 
    }
}