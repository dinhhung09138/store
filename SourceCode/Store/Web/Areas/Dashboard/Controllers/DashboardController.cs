using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Dashboard.Controllers
{
    //[RoutePrefix("home")]
    public class DashboardController : Controller
    {
        // GET: Dashboard/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}