﻿using Microsoft.AspNetCore.Mvc;

namespace ETrade.Core.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            if (!User.IsInRole("Admin"))
                return Redirect("~/Home/Index");
            return View();
        }
    }
}
