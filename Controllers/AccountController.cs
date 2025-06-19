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
using UDAS.Entities;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
                var hashedPassword = ComputeSha256Hash(model.Password);
                var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName && u.PasswordHash == hashedPassword  
                                                        && (u.RoleId == "0" || u.RoleId == "2"));
                
                if (user != null)
                {   

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Surname, user.Surname),
                        new Claim(ClaimTypes.Role, user.RoleId.ToString()),
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
                var hashedPassword = ComputeSha256Hash(model.Password);
                var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName && u.PasswordHash == hashedPassword  
                                                        && u.RoleId == "1");
                
                if (user != null)
                {   

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Surname, user.Surname),
                        new Claim(ClaimTypes.Role, user.RoleId.ToString())
                    };
                    
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProporties = new AuthenticationProperties();
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),
                    authProporties);


                    return RedirectToAction("Index", "InstructorSchedule");
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










        [Authorize]
        [HttpGet]
        [Route("Register")]
        public IActionResult  Register()
        {
            return View();
        }









        [Authorize]
        [HttpPost]
        [Route("Register")]
        public IActionResult  Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.UserName == model.Username);
                
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Bu kullanıcı adı zaten alınmış.");
                    return View(model);
                }

                // Şifre hashleme
                var hashedPassword = ComputeSha256Hash(model.Password);

                var user = new User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    RoleId = model.RoleId,
                    UserName = model.Username,
                    PasswordHash = hashedPassword
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return View();
            }

            return View(model);
        }






        [Authorize]
        [HttpPost]
        [Route("Authorization")]
        public IActionResult UpdateRole(UserViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == model.Id);
            if (user != null)
            {
                user.RoleId = model.RoleId;
                _context.SaveChanges();
            }

            return RedirectToAction("Authorization");
        }






        [Authorize]
        [HttpGet]
        [Route("Authorization")]
        public async Task<IActionResult> Authorization(UserViewModel model)
        {
            var users = await _context.Users.ToListAsync();

            var viewModelList = users.Select(user => new UserViewModel
            {
                Id = (int)user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Username = user.UserName,
                Password = "",
                RoleId = user.RoleId
            }).ToList();

            return View(viewModelList);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }





        private string ComputeSha256Hash(string rawData)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }
    }
}
