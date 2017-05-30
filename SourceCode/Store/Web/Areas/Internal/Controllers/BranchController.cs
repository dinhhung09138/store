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

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Add()
        {
            BranchModel model = new BranchModel()
            {
                Insert = true
            };
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {

            BranchSrv _serBranch = new BranchSrv();
            BranchModel model = _serBranch.Item(new Guid(id));
            model.Insert = false;
            return PartialView(model);
        }

        /// <summary>
        /// Find location by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [AjaxAuthorize]
        [HttpPost]
        public JsonResult FindLocationByName(string name)
        {
            LocationSrv _srvLocation = new LocationSrv();
            return this.Json(_srvLocation.FindByName(name.ToLower()), JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        [HttpPost]
        public JsonResult Save(BranchModel model)
        {
            Model.User.UserLoginModel user = Session["user"] as Model.User.UserLoginModel;
            if (model.Insert)
            {
                model.ID = Guid.NewGuid();
                model.CreateBy = user.ID;
            }
            else
            {
                model.UpdatedBy = user.ID;
            }
            BranchSrv _srvBranch = new BranchSrv();
            return this.Json(_srvBranch.Save(model), JsonRequestBehavior.AllowGet);
        }

    }
}