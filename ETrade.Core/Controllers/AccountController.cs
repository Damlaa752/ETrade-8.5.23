﻿using ETrade.Entity.Models.Identity;
using ETrade.Entity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult SignUp()
        {
            if (User.Identity.Name == null)
                return View();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            var user = new User(model.Username)
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password=model.Password,
                PhoneNumber = model.Phone,      
            };
            var result = await _userManager.CreateAsync(user, user.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn");
            }
            return View(model);
        }
        public IActionResult SignIn()
        {
            if (User.Identity.Name == null)
                return View();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> SignIn (SignInViewModel model)
        {
            User user;
            if (model.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(model.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.UsernameOrEmail);
            }
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password,true,true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
    }
}

