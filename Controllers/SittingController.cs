using System.Diagnostics;
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

    public SittingController(SittingPlanRepository sittingPlanRepository)
    {
        _sittingPlanRepository = sittingPlanRepository;
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
}
