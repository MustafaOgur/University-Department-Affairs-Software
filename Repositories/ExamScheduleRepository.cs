using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using UDAS.Data;
using UDAS.Entities;

namespace UDAS.Repositories
{
    public class ExamScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetCoursesAsync(){
            return await _context.Courses.ToListAsync();
        }

        public async Task<List<Classroom>> GetClassroomsAsync(){
            return await _context.Classrooms.ToListAsync();
        }

        public async Task<List<User>> GetUsersAsync(){
            return await _context.Users.ToListAsync();
        }

        public async Task<List<CourseTime>> GetCourseTimesAsync(){
            return await _context.CourseTimes.ToListAsync();
        }

        public bool IsAvailable(ExamSchedule examSchedule)
        {   
            bool conflictExists = _context.ExamSchedules
            .Where(s => s.ClassroomId == examSchedule.ClassroomId &&
                        s.Date == examSchedule.Date &&
                        s.StartTime == examSchedule.StartTime)
            .Any();

            return !conflictExists;
        }

        public async Task<string> AddExamScheduleAsync(ExamSchedule examSchedule)
        {   
            if (IsAvailable(examSchedule))
            {
                _context.Add(examSchedule);
                await _context.SaveChangesAsync();

                return null;
            }
            else
            {
                return "Bu sınıf girilen tarih için dolu, lütfen farklı bir zaman aralığı seçin";
            }
        }

        public async Task DeleteExamSchedule()
        {
            var examSchedules = await _context.ExamSchedules.ToListAsync();

            if(examSchedules.Any()) {

                _context.RemoveRange(examSchedules);
                await _context.SaveChangesAsync();
            }
        }
    }
}