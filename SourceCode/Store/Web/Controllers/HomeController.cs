using Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    
    public class HomeController : Controller
    {
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            UserLogin model = new UserLogin();
            if (TempData["model"] != null)
            {
                model = TempData["model"] as UserLogin;
                TempData["message"] = TempData["message"];
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserLogin model)
        {
            if (model.UserName == "admin" || model.Password != "pass")
            {
                TempData["model"] = model;
                TempData["message"] = Resources.Login.msgWrongLogin;
            }
            return View("login");
        }
    }
}