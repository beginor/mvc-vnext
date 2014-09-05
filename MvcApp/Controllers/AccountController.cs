using Microsoft.AspNet.Mvc;
using MvcApp.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcApp.Controllers {

    public class AccountController : Controller {

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public IActionResult Register() {
            return View();
        }
    }

}