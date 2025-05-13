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
        return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> DisplayExam()
    {   
        return View();
    }

}