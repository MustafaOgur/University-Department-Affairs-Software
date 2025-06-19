using System.Diagnostics;
using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using UDAS.Data;
using UDAS.Entities;
using UDAS.Models;
using UDAS.Repositories;
using UDAS.ViewModels;

namespace UDAS.Controllers;

public class SittingController : Controller
{
    private readonly SittingPlanRepository _sittingPlanRepository;
    private readonly ExamScheduleRepository _examScheduleRepository;

    public SittingController(SittingPlanRepository sittingPlanRepository, ExamScheduleRepository examScheduleRepository)
    {
        _sittingPlanRepository = sittingPlanRepository;
        _examScheduleRepository = examScheduleRepository;
    }

    [Authorize]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> AddSitting()
    {
        var classrooms = await _sittingPlanRepository.GetClassroomsAsync();

        var viewModel = new SittingAddViewModel
        {
            Classrooms = classrooms
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddSitting(SeatingPlan seatingPlan, string action)
    {
        switch (action)
        {
            case "save":

                var errorMessage = await _sittingPlanRepository.AddSeatingplanAsync(seatingPlan);

                if (errorMessage != null)
                {
                    ModelState.AddModelError("", errorMessage);

                    var classrooms = await _sittingPlanRepository.GetClassroomsAsync();
                    var viewModel = new SittingAddViewModel
                    {
                        Classrooms = classrooms
                    };

                    return View(viewModel);
                }
                break;
        }

        return RedirectToAction("AddSitting");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> DisplaySeatingPlan()
    {
        var seatingPlans = await _sittingPlanRepository.GetSeatingPlansAsync();

        var viewModel = new SeatingDisplayViewModel
        {
            SeatingPlans = seatingPlans
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DisplaySeatingPlan(int planId)
    {
        await _sittingPlanRepository.DeleteSeatingplanAsync(planId);

        return RedirectToAction("DisplaySeatingPlan");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> LoadPartialView(int classroomId)
    {
        var classrooms = await _sittingPlanRepository.GetClassroomsAsync();
        var selectedClassroom = classrooms.Where(c => c.Id == classroomId).FirstOrDefault();

        if (selectedClassroom == null)
        {
            return Content("Sınıf Bulunamadı");
        }

        var partialViewName = "_" + selectedClassroom.RoomName.Replace(" ", "") + "ClassroomTemplate";
        ViewData["Mode"] = "edit";

        SeatingPlan seatingPlan = null;

        return PartialView(partialViewName, seatingPlan);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> LoadDisplayPartialView(int planId)
    {
        if (planId == null)
        {
            return Content("Plan Bulunamadı");
        }

        var plan = await _sittingPlanRepository.GetSeatingPlanByIdAsync(planId);

        var classroomName = plan.SeatAssignments.FirstOrDefault().Classroom.RoomName;
        var partialViewName = "_" + classroomName.Replace(" ", "") + "ClassroomTemplate";

        ViewData["Mode"] = "view";

        return PartialView(partialViewName, plan);
    }
    

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> InstructorSeatingPlans()
    {
        var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdString, out int lecturerId))
        {
            return Unauthorized();
        }

        var examSchedules = await _examScheduleRepository.GetExamSchedulesByLecturerIdAsync(lecturerId);

        var seatingPlans = examSchedules
            .Where(es => es.SeatingPlan != null)
            .Select(es => es.SeatingPlan)
            .Distinct()
            .ToList();

        var viewModel = new SeatingDisplayViewModel
        {
            SeatingPlans = seatingPlans
        };

        return View(viewModel);
    }
}
