using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using UDAS.Data;
using UDAS.Entities;
using UDAS.Models;
using UDAS.Repositories;
using UDAS.ViewModels;

namespace UDAS.Controllers;

public class ExamController : Controller
{
    private readonly ExamScheduleRepository _examScheduleRepository;
    private readonly ApplicationDbContext _context;

    public ExamController(ExamScheduleRepository examScheduleRepository, ApplicationDbContext context)
    {
        _examScheduleRepository = examScheduleRepository;
        _context = context;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index()
    {

        return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> AddExam()
    {
        var courseTimes = await _examScheduleRepository.GetCourseTimesAsync();
        var courses = await _examScheduleRepository.GetCoursesAsync();
        var classrooms = await _examScheduleRepository.GetClassroomsAsync();
        var supervisors = await _examScheduleRepository.GetUsersAsync();
        var seatingPlans = await _examScheduleRepository.GetSeatingPlansAsync();

        var viewModel = new ExamAddViewModel
        {
            CourseTimes = courseTimes,
            Courses = courses,
            Classrooms = classrooms,
            Supervisors = supervisors,
            SeatingPlans = seatingPlans
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddExam(ExamSchedule examSchedule, string action)
    {
        switch (action)
        {
            case "save":

                var errorMessage = await _examScheduleRepository.AddExamScheduleAsync(examSchedule);

                if (errorMessage != null)
                {

                    ModelState.AddModelError("", errorMessage);

                    var courseTimes = await _examScheduleRepository.GetCourseTimesAsync();
                    var courses = await _examScheduleRepository.GetCoursesAsync();
                    var classrooms = await _examScheduleRepository.GetClassroomsAsync();
                    var supervisors = await _examScheduleRepository.GetUsersAsync();
                    var seatingPlans = await _examScheduleRepository.GetSeatingPlansAsync();

                    var viewModel = new ExamAddViewModel
                    {
                        CourseTimes = courseTimes,
                        Courses = courses,
                        Classrooms = classrooms,
                        Supervisors = supervisors,
                        SeatingPlans = seatingPlans
                    };

                    return View(viewModel);
                }

                break;

            case "delete":

                await _examScheduleRepository.DeleteExamSchedule();

                break;
        }

        return RedirectToAction("AddExam");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> DisplayExam()
    {
        var examSchedules = await _examScheduleRepository.GetExamSchedulesAsync();

        var viewModel = new ExamDisplayViewModel
        {

            ExamSchedules = examSchedules

        };

        return View(viewModel);
    }
    
    [Authorize(Roles = "1")] // Akademik personel
    [HttpGet]
    [Route("AcademicExamSchedule")]
    public async Task<IActionResult> AcademicExamSchedule()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        int parsedUserId = int.Parse(userId);

        var examNotes = await _context.Notes
            .Where(n => n.UserId == parsedUserId && n.NoteType == "Sınav")
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        var examSchedules = await _examScheduleRepository.GetExamSchedulesByLecturerIdAsync(parsedUserId);

        var viewModel = new ExamDisplayViewModel
        {
            ExamSchedules = examSchedules,
            ExamNotes = examNotes
        };

        return View(viewModel);
    }

}