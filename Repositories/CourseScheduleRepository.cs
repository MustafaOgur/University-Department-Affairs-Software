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
    public class CourseScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseScheduleRepository(ApplicationDbContext context)
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

        public async Task<List<WeekDay>> GetWeekDaysAsync(){
            return await _context.WeekDays.ToListAsync();
        }

        public async Task<List<CourseTime>> GetCourseTimesAsync(){
            return await _context.CourseTimes.ToListAsync();
        }

        public async Task<List<Schedule>> GetSchedulesAsync(){
            return await _context.Schedules
            .Include(s => s.Course)
            .Include(s => s.Classroom)
            .Include(s => s.Lecturer)
            .ToListAsync();
        }

        public bool IsAvailable(Schedule schedule)
        {   
            bool conflictExists = _context.Schedules
            .Include(s => s.Course)
            .Where(s => s.LecturerId == schedule.LecturerId &&
                        s.Day == schedule.Day &&
                        s.StartTime == schedule.StartTime)
            .Any();

            return !conflictExists;
        }

        public async Task<string> AddScheduleAsync(Schedule schedule)
        {   
            if (IsAvailable(schedule))
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();

                return null;
            }
            else
            {
                return "Bu saatler girilen gün için dolu, lütfen farklı bir zaman aralığı seçin";
            }
        }

        public async Task DeleteScheduleByName(string scheduleName)
        {
            var schedules = await _context.Schedules.Where(s => s.scheduleName == scheduleName).ToListAsync();

            if (schedules.Any())
            {
                _context.Schedules.RemoveRange(schedules);
                await _context.SaveChangesAsync();
            }
        }

    }
}