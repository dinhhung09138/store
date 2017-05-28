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

namespace Web.Areas.Internal.Controllers
{
    public class BranchController : Controller
    {
        // GET: Partner/Customer
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [CustomAuthorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [AjaxAuthorize]
        [HttpPost]
        public JsonResult Index(CustomJqueryDataTableRequest requestData)
        {
            requestData = requestData.SetOrderingColumnName();
            BranchSrv _srvBranch = new BranchSrv();
            Dictionary<string, object> _return = _srvBranch.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<BranchModel> itemResponse = _return["data"] as JqueryDataTableResponse<BranchModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<BranchModel>(), JsonRequestBehavior.AllowGet);
        }

    }
}