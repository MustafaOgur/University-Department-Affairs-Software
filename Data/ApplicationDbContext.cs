using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UDAS.Entities;

namespace UDAS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<WeekDay> WeekDays { get; set; }
        public DbSet<CourseTime> CourseTimes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ExamSchedule> ExamSchedules { get; set; }
        public DbSet<SeatingPlan> SeatingPlans { get; set; }
        public DbSet<SeatAssignment> SeatAssignments { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}