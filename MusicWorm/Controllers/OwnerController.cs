using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicWorm.Models;
using MusicWorm.ViewModels;

namespace MusicWorm.Controllers
{
    [Authorize(Roles = "Owner, Admin")]
    public class OwnerController : Controller
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly SignInManager<StoreUser> _signInManager;

        public OwnerController(UserManager<StoreUser> userManager,
            SignInManager<StoreUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new StoreUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                    
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}