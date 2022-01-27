using EnnoTest3.Areas.Manage.ViewModels;
using EnnoTest3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnnoTest3.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, DataContext context, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel adminVM)
        {
            bool isAdmin = await _userManager.Users.AnyAsync(x => x.IsAdmin);
            if (isAdmin == true)
            {
                AppUser admin = await _userManager.FindByNameAsync(adminVM.Username);
                if (admin == null)
                {
                    ModelState.AddModelError("", "Password or Username is Incorrect!");
                    return View(adminVM);
                }
                var result = await _signInManager.PasswordSignInAsync(admin, adminVM.Password, false, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Password or Username is Incorrect!");
                    return View(adminVM);
                }
                return RedirectToAction("index", "dashboard");
            }
            ModelState.AddModelError("", "Password or Username is Incorrect!");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

        public async Task<IActionResult> CreateRole()
        {
            //var role1 = await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            //var role2 = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            //var role3 = await _roleManager.CreateAsync(new IdentityRole("Member"));

            var admin = new AppUser { Fullname = "Super Admin", UserName = "SuperAdmin" };
            var result = await _userManager.CreateAsync(admin, "Admin123");
            var resultRole = await _userManager.AddToRoleAsync(admin, "SuperAdmin");
            return Ok(resultRole);
        }

    }
}
