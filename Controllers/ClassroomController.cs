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

public class ClassroomController : Controller
{   
    [Authorize]
    [HttpGet]
    public IActionResult Classroom()
    {
        return View();
    }

}