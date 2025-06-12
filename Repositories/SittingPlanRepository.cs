using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using UDAS.Data;
using UDAS.Entities;
using UDAS.ViewModels;

namespace UDAS.Repositories
{
    public class SittingPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public SittingPlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Classroom>> GetClassroomsAsync()
        {
            return await _context.Classrooms.ToListAsync();
        }

        public async Task<List<SeatingPlan>> GetSeatingPlansAsync()
        {
            return await _context.SeatingPlans.Include(p => p.SeatAssignments).ToListAsync();
        }

        public async Task<string> AddSeatingplanAsync(SeatingPlan seatingPlan)
        {
            try
            {
                _context.SeatingPlans.Add(seatingPlan);
                await _context.SaveChangesAsync();

                return null;
            }
            catch (Exception e)
            {
                return "Bir hata oluştu: " + e;
            }
        }

        public async Task DeleteSeatingplanAsync(int planId)
        {
            var seatingPlan = await _context.SeatingPlans.Include(p => p.SeatAssignments).Where(p => p.Id == planId).FirstOrDefaultAsync();

            if (seatingPlan != null)
            {
                _context.SeatAssignments.RemoveRange(seatingPlan.SeatAssignments);
                _context.SeatingPlans.Remove(seatingPlan);
                await _context.SaveChangesAsync();
            }
        }
        
    }
}