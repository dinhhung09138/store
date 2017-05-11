using Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;
//
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;

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

        [AllowAnonymous]
        public ActionResult Chat()
        {
            return View();
        }

        [AllowAnonymous]
        public JsonResult Click()
        {
            try
            {
                Message();
                return this.Json("Done", JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return this.Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            
        }

        private static async Task Message()
        {
            try
            {
                var hubConnection = new HubConnection("http://localhost:6424");
                IHubProxy myHub = hubConnection.CreateHubProxy("NotificationHub");
                hubConnection.Start().Wait();
                await myHub.Invoke("Send", "Hung", "Toi la tran dinh hung");
                hubConnection.Stop();
            }
            catch(Exception ex)
            {

            }
        }
    }

    public static class Notification
    {
        private static async Task Message()
        {
            var hubConnection = new HubConnection("");
            IHubProxy myHub = hubConnection.CreateHubProxy("NotifycationHub");
            hubConnection.Start().Wait();
            await myHub.Invoke("Send", "Hung", "Toi la tran dinh hung");
            hubConnection.Stop();
        }
    }
}