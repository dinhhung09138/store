using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Goods.Controllers
{
    public class ProductController : Controller
    {
        // GET: Goods/Product
        public ActionResult Index()
        {
            return View();
        }
    }
}