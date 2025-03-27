using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UDAS.Data;
using UDAS.Models;

namespace UDAS.Controllers;

public class MainMenuController : Controller
{   
    [Authorize]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
