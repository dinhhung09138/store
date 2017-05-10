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
            UserLoginModel model = new UserLoginModel();
            if (TempData["model"] != null)
            {
                model = TempData["model"] as UserLoginModel;
                TempData["message"] = TempData["message"];
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserLoginModel model)
        {
            if (model.UserName == "admin" || model.Password != "pass")
            {
                TempData["model"] = model;
                TempData["message"] = Resources.Login.msgWrongLogin;
            }
            return View("login");
        }

        [AllowAnonymous]
        public ActionResult Demo()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Test01()
        {
            return PartialView();
        }

        [AllowAnonymous]
        
        public ActionResult Test02()
        {
            return PartialView();
        }
    }
}