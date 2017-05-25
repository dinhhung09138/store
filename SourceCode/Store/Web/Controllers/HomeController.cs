using Model;
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
using Common.JqueryDataTable;
using DataAccess;
using DataAccess.User;
using Common.Status;

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
        public ActionResult GoogleChart()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SetInterval()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult DataTable()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Calendar()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetData(CustomJqueryDataTableRequest requestData)
        {
            requestData = requestData.SetOrderingColumnName();
            CustomerSrv _api = new CustomerSrv();
            Dictionary<string, object> _return = _api.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<CustomerModel> itemResponse = _return["data"] as JqueryDataTableResponse<CustomerModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<CustomerModel>(), JsonRequestBehavior.AllowGet);
        }

        #region " [ Login ] "

        [AllowAnonymous]
        public ActionResult Login()
        {
            Session["user"] = null;
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
            string pass = Common.Security.PasswordSecurity.GetHashedPassword(model.Password);

            AccountSrv _srvAccount = new AccountSrv();
            UserLoginModel _return = _srvAccount.Login(model.UserName, pass);
            if (_return == null)
            {
                TempData["model"] = model;
                TempData["message"] = Resources.Login.msgWrongLogin;
                return RedirectToAction("login", "home");
            }
            Session["user"] = _return;
            return RedirectToAction("index", "home");
        }


        #endregion

        #region " [ For got password ] "

        /// <summary>
        /// Forgot password
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            ViewBag.email = "";
            if (TempData["email"] != null)
            {
                ViewBag.email = TempData["email"];
            }

            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            return View();
        }

        #endregion

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RequiredLogin()
        {
            return this.Json(new { login = true }, JsonRequestBehavior.AllowGet);
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
            catch (Exception ex)
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
            catch (Exception ex)
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