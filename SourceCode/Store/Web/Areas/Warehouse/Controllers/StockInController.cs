using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Warehouse.Controllers
{
    public class StockInController : Controller
    {
        // GET: Warehouse/StockIn
        public ActionResult Index()
        {
            return View();
        }
    }
}