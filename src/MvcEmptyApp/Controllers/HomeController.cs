﻿using Microsoft.AspNet.Mvc;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcApp.Controllers {

    public class HomeController : Controller {

        // GET: /<controller>/
        public IActionResult Index() {
            return Content("Hello, ASP.NET Mvc under " + Environment.OSVersion.ToString());
        }

        public IActionResult About() {
            ViewBag.Message = "Your application description page.";
            return Content("Your application description page.");
        }

        public IActionResult Contact() {
            ViewBag.Message = "Your contact page.";
            return View("Your contact page.");
        }
    }
}
