using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;
using DataAccess;
using Common.Status;
using Common.JqueryDataTable;
using Model;

namespace Web.Areas.Partner.Controllers
{
    [CustomAuthorize]
    public class CustomerController : Controller
    {
        // GET: Partner/Customer
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Index(CustomJqueryDataTableRequest requestData)
        {
            CustomerSrv _srvCustomer = new CustomerSrv();
            Dictionary<string, object> _return = _srvCustomer.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<CustomerModel> itemResponse = _return["data"] as JqueryDataTableResponse<CustomerModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<CustomerModel>(), JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult Save(CustomerModel model)
        {
            return Json("ok");
        }

    }
}