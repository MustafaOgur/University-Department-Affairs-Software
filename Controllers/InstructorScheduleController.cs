using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UDAS.Data;
using UDAS.Repositories;
using UDAS.ViewModels;

namespace UDAS.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class InstructorScheduleController : Controller
    {
        private readonly ILogger<InstructorScheduleController> _logger;
        private readonly CourseScheduleRepository _courseScheduleRepository;
        private readonly ApplicationDbContext _context;

        public InstructorScheduleController(CourseScheduleRepository courseScheduleRepository, 
                                            ApplicationDbContext context,
                                            ILogger<InstructorScheduleController> logger)
        {
            _logger = logger;
            _courseScheduleRepository = courseScheduleRepository;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [Route("Index")]
        public async Task<IActionResult> Index(int? lecturerId)
        {

            if (lecturerId == null)
            {
                var lecturerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (lecturerIdClaim == null || !int.TryParse(lecturerIdClaim.Value, out int idFromClaim))
                {
                    return View("Error", "Geçerli bir kullanıcı bulunamadı.");
                }
                lecturerId = idFromClaim;
            }


            var schedules = await _courseScheduleRepository.GetInstructorSchedulesAsync(lecturerId.Value);
            
            

            var groupped = schedules
            .Where(s => s.Course != null)
            .GroupBy(s => s.Course!.Year)
            .Select(g => new YearlyScheduleGroup{
                Year = g.Key,
                Schedules = g.ToList()
            })
            .ToList();

            var viewModel = new ScheduleDisplayViewModel{
                Groups = groupped,
            };

            return View(viewModel);

        }


        [HttpGet]
        [Authorize]
        [Route("Lecturers")]
        public async Task<IActionResult> Lecturers(UserViewModel model) 
        {
            var users = await _context.Users.Where(u => u.RoleId == "1").ToListAsync();

            var viewModelList = users.Select(user => new UserViewModel
            {
                Id = (int)user.Id,
                Name = user.Name,
                Surname = user.Surname,
            }).ToList();

            return View(viewModelList);
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
