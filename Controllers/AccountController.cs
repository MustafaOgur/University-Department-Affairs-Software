using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UDAS.ViewModels;
using UDAS.Data;
using UDAS.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace UDAS.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // ---------------------------- İdari Giriş ------------------------------------
        [AllowAnonymous]
        [HttpGet]
        [Route("AdministrativeLogin")]
        public IActionResult AdministrativeLogin()
        {
            return View();
        }

        [HttpPost]
        [Route("AdministrativeLogin")]
        public async Task<IActionResult> AdministrativeLogin(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName && u.PasswordHash == model.Password);
                
                if (user != null)
                {   

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Name, user.RoleId),
                    };
                    
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProporties = new AuthenticationProperties();
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                    authProporties);


                    return RedirectToAction("Menu","Account");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre !");
                }
            }

            return View(model);
        }

        // ---------------------------- Akademik Giriş ------------------------------------
        [HttpGet]
        [Route("AcademicLogin")]
        public IActionResult AcademicLogin()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AcademicLogin")]
        public async Task<IActionResult> AcademicLogin(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName && u.PasswordHash == model.Password);
                
                if (user != null)
                {   

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Name, user.RoleId),
                    };
                    
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProporties = new AuthenticationProperties();
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                    authProporties);


                    return RedirectToAction("Menu","Account");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre !");
                }
            }

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        [HttpGet]
        [Route("Menu")]
        public IActionResult Menu() => RedirectToAction("Index", "MainMenu");


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}