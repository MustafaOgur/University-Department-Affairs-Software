using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public ExamController(ExamScheduleRepository examScheduleRepository)
    {
        _examScheduleRepository = examScheduleRepository;
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

        var viewModel = new ExamAddViewModel{
            CourseTimes = courseTimes,
            Courses = courses,
            Classrooms = classrooms,
            Supervisors = supervisors
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddExam(ExamSchedule examSchedule, string action)
    {   
        switch(action) {
            case "save":

                var errorMessage = await _examScheduleRepository.AddExamScheduleAsync(examSchedule);

                if(errorMessage != null){

                    ModelState.AddModelError("", errorMessage);

                    var courseTimes = await _examScheduleRepository.GetCourseTimesAsync();
                    var courses = await _examScheduleRepository.GetCoursesAsync();
                    var classrooms = await _examScheduleRepository.GetClassroomsAsync();
                    var supervisors = await _examScheduleRepository.GetUsersAsync();

                    var viewModel = new ExamAddViewModel{
                        CourseTimes = courseTimes,
                        Courses = courses,
                        Classrooms = classrooms,
                        Supervisors = supervisors
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
        return View();
    }

}