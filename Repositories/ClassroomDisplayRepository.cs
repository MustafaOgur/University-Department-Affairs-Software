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
    public class ClassroomDisplayRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassroomDisplayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Classroom>> GetClassroomsAsync(){
            return await _context.Classrooms.ToListAsync();
        }

        public async Task<Classroom?> GetClassroomAsync(int classroomId) 
        {
            return await _context.Classrooms
            .Where(c => c.Id == classroomId)
            .FirstOrDefaultAsync();
        }

        public async Task<List<Schedule>> GetClassroomSchedulesAsync(int classroomId)
        {
            return await _context.Schedules
            .Where(s => s.ClassroomId == classroomId)
            .Include(s => s.Course)
            .Include(s => s.Lecturer)
            .Include(s => s.Classroom)
            .ToListAsync();
        }

        public async Task<List<string>> GetClassroomWeekDaysAsync(int classroomId) 
        {   
            var dayKeys = await _context.Schedules
            .Where(s => s.ClassroomId == classroomId)
            .GroupBy(s => s.Day)
            .Select(g => g.Key)
            .ToListAsync();

            var days = new List<string>();

            foreach(var key in dayKeys){
                switch(key){
                    case "1":
                        days.Add("Pazartesi");
                        break;
                    case "2":
                        days.Add("Salı");
                        break;
                    case "3":
                        days.Add("Çarşamba");
                        break;
                    case "4":
                        days.Add("Perşembe");
                        break;
                    case "5":
                        days.Add("Cuma");
                        break;
                }
            }

            return days;
        }

        public async Task<List<CourseTime>> GetClassroomCourseTimesAsync(int classroomId) 
        {   
            return await _context.Schedules
            .Where(s => s.ClassroomId == classroomId)
            .GroupBy(s => new {s.StartTime, s.EndTime} )
            .Select(g => new CourseTime
            {
                StartTime = g.Key.StartTime,
                EndTime = g.Key.EndTime
            })
            .ToListAsync();
        }

        public async Task<List<User>> GetClassroomLecturersAsync(int classroomId) 
        {
            return await _context.Schedules
            .Where(s => s.ClassroomId == classroomId)
            .GroupBy(s => new{s.Lecturer!.Name, s.Lecturer!.Surname})
            .Select(g => new User {
                Name = g.Key.Name,
                Surname = g.Key.Surname
            })
            .ToListAsync();
        }
    }
}