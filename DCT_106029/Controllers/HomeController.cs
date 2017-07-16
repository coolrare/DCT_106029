using DCT_106029.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DCT_106029.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password == "123")
                {
                    FormsAuthentication.RedirectFromLoginPage(user.Username, false);
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}
