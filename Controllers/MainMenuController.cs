using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using UDAS.Data;
using UDAS.Entities;
using UDAS.Models;
using UDAS.Repositories;
using UDAS.ViewModels;

namespace UDAS.Controllers;

public class MainMenuController : Controller
{   

    private readonly CourseScheduleRepository _courseScheduleRepository;
    private readonly ApplicationDbContext _context;

    public MainMenuController(CourseScheduleRepository courseScheduleRepository, ApplicationDbContext context)
    {
        _courseScheduleRepository = courseScheduleRepository;
        _context = context;
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
    [HttpPost]
    public async Task<IActionResult> AddSchedule(Schedule schedule, string action)
    {   
        switch (action)
        {
            case "add":

                    var errorMessage = await _courseScheduleRepository.AddScheduleAsync(schedule);

                    if (errorMessage != null)
                    {
                        ModelState.AddModelError("", errorMessage);

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

                break;

            // case "save":

            //     break;

            case "abort":

                    await _courseScheduleRepository.DeleteScheduleByName(schedule.scheduleName);

                break;
        }

        return RedirectToAction("AddSchedule");
    }


    [Authorize]
    [HttpGet]
    public async Task<IActionResult> DisplaySchedules()
    {   

        var schedules = await _courseScheduleRepository.GetSchedulesAsync();

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
}
