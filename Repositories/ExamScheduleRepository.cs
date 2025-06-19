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

        public async Task<List<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<List<Classroom>> GetClassroomsAsync()
        {
            return await _context.Classrooms.ToListAsync();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<CourseTime>> GetCourseTimesAsync()
        {
            return await _context.CourseTimes.ToListAsync();
        }

        public async Task<List<SeatingPlan>> GetSeatingPlansAsync()
        {
            return await _context.SeatingPlans.ToListAsync();
        }

        public async Task<List<ExamSchedule>> GetExamSchedulesAsync()
        {
            return await _context.ExamSchedules
            .Include(s => s.Course)
            .Include(s => s.Classroom)
            .Include(s => s.Supervisor)
            .Include(s => s.SeatingPlan)
            .ThenInclude(s => s.SeatAssignments)
            .ToListAsync();
        }

        public bool IsAvailable(ExamSchedule examSchedule)
        {
            // Aynı anda bir sınıfta 2 tane sınav olamaz
            bool conflictExists1 = _context.ExamSchedules
            .Where(s => s.ClassroomId == examSchedule.ClassroomId &&
                        s.Date == examSchedule.Date &&
                        s.StartTime == examSchedule.StartTime)
            .Any();

            // Bir gözetmen aynı anda 2 sınavda bulunamaz
            bool conflictExists2 = _context.ExamSchedules
            .Where(s => s.SupervisorId == examSchedule.SupervisorId &&
                        s.Date == examSchedule.Date &&
                        s.StartTime == examSchedule.StartTime)
            .Any();

            // Bir dersten birden fazla sınav olamaz
            bool conflictExists3 = _context.ExamSchedules
            .Where(s => s.CourseId == examSchedule.CourseId)
            .Any();

            // Seçilen Sınıf Oturma Planıyla eşleşiyor mu?
            var seatingPlan = _context.SeatingPlans.Include(s => s.SeatAssignments).FirstOrDefault(s => s.Id == examSchedule.SeatingPlanId);
            bool conflictExists4 = !seatingPlan.SeatAssignments.All(s => s.ClassroomId == examSchedule.ClassroomId);


            bool conflictExists = conflictExists1 || conflictExists2 || conflictExists3 || conflictExists4;

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

            if (examSchedules.Any())
            {

                _context.RemoveRange(examSchedules);
                await _context.SaveChangesAsync();
            }
        }
        

        public async Task<List<ExamSchedule>> GetExamSchedulesByLecturerIdAsync(int lecturerId)
        {
            // Öğretim elemanının verdiği derslerin Id'leri
            var lecturerCourseIds = await _context.Schedules
                .Where(s => s.LecturerId == lecturerId && s.CourseId != null)
                .Select(s => s.CourseId.Value)
                .Distinct()
                .ToListAsync();

            // Bu derslerin sınavlarını getir
            var examSchedules = await _context.ExamSchedules
                .Include(es => es.Course)
                .Include(es => es.Classroom)
                .Include(es => es.SeatingPlan)
                .Include(es => es.Supervisor)
                .Where(es => es.CourseId != null && lecturerCourseIds.Contains(es.CourseId.Value))
                .ToListAsync();

            return examSchedules;
        }
    }
}