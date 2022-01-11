﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;

namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View("Register", model);
            }

            var result = _userManager.CreateAsync(new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PasswordHash = model.Password,
            });

            if(!result.Result.Succeeded)
            {
                foreach (var item in result.Result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View("Register", model);
            }

            return RedirectToAction("home");
        }
    }
}
