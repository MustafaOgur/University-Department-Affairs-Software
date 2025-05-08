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

public class ClassroomController : Controller
{   
    private readonly ClassroomDisplayRepository _classroomDisplayRepository;

    public ClassroomController(ClassroomDisplayRepository classroomDisplayRepository)
    {
        _classroomDisplayRepository = classroomDisplayRepository;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ClassroomSelection()
    {   
        var classrooms = await _classroomDisplayRepository.GetClassroomsAsync();

        var viewModel = new ClassroomSelectionViewModel {
            Classrooms = classrooms
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Classroom(int classroomId)
    {   
        var classroom = await _classroomDisplayRepository.GetClassroomAsync(classroomId);
        var schedules = await _classroomDisplayRepository.GetClassroomSchedulesAsync(classroomId);

        var days = await _classroomDisplayRepository.GetClassroomWeekDaysAsync(classroomId);
        var lecturers = await _classroomDisplayRepository.GetClassroomLecturersAsync(classroomId);
        var courseTimes = await _classroomDisplayRepository.GetClassroomCourseTimesAsync(classroomId);

        var viewModel = new ClassroomViewModel {
            Classroom = classroom,
            Schedules = schedules,
            CourseTimes = courseTimes,
            Lecturers = lecturers,
            Days = days
        };

        return View(viewModel);
    }

}