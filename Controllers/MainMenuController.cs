using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UDAS.Data;
using UDAS.Models;
using UDAS.Repositories;
using UDAS.ViewModels;

namespace UDAS.Controllers;

public class MainMenuController : Controller
{   

    private readonly CourseScheduleRepository _courseScheduleRepository;

    public MainMenuController(CourseScheduleRepository courseScheduleRepository)
    {
        _courseScheduleRepository = courseScheduleRepository;
    }



    [Authorize]
    [HttpGet]
    public IActionResult Index()
    {
        
        return View();
    }


    [Authorize]
    [HttpGet]
    public async Task<IActionResult> AddSchedule()
    {
        var courses = await _courseScheduleRepository.GetCoursesAsync();
        var classrooms = await _courseScheduleRepository.GetClassroomsAsync();
        var weekdays = await _courseScheduleRepository.GetWeekDaysAsync();
        var courseTimes = await _courseScheduleRepository.GetCourseTimesAsync();
        var users = await _courseScheduleRepository.GetUsersAsync();
        

        var viewModel = new CourseScheduleViewModel{
            Courses = courses,
            Classrooms = classrooms,
            WeekDays = weekdays,
            CourseTimes = courseTimes,
            Users = users
        };

        return View(viewModel);
    }


    [Authorize]
    [HttpGet]
    public IActionResult DisplaySchedules()
    {
        return View();
    }
}
