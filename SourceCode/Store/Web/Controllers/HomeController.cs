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

        /// <summary>
        /// http
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveData(List<ListData> model)
        {
            return this.Json("");
        }

        [AllowAnonymous]
        public ActionResult Editor()
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

    public class Event
    {
        /// <summary>
        /// String/Integer. Optional
        /// Uniquely identifies the given event. Different instances of repeating events should all have the same id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// String. Required.
        /// The text on an event's element
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The date/time an event begins. Required.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The exclusive date/time an event ends. Optional.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// String. Optional.
        /// A URL that will be visited when this event is clicked by the user. For more information on controlling
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// String/Array. Optional.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// true or false. Optional.
        /// Overrides the master editable option for this single event.
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        /// Sets an event's background and border color just like the calendar-wide eventColor option.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Sets an event's border color just like the the calendar-wide eventBorderColor option.
        /// </summary>
        public string BorderColor { get; set; }

        /// <summary>
        /// Sets an event's background color just like the calendar-wide eventBackgroundColor option.
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Sets an event's text color just like the calendar-wide eventTextColor option.
        /// </summary>
        public string TextColor { get; set; }

        /// <summary>
        /// true or false. Optional.
        /// If false, prevents this event from being dragged/resized over other events. 
        /// Also prevents other events from being dragged/resized over this event.
        /// </summary>
        public bool Overlap { get; set; }

        /// <summary>
        /// Allows alternate rendering of the event, like background events.
        /// Can be empty, "background", or "inverse-background"
        /// </summary>
        public string Rendering { get; set; }

        /// <summary>
        /// true or false. Optional.
        /// </summary>
        public string ResourceEditable { get; set; }

        /// <summary>
        /// true or false. Optional.
        /// Allow events' start times to be editable through dragging.
        /// </summary>
        public bool StartEditable { get; set; }
    }

    public class ListData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}