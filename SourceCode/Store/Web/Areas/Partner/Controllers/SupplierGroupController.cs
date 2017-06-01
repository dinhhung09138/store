using Common.JqueryDataTable;
using Common.Status;
using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Areas.Partner.Controllers
{
    public class SupplierGroupController : Controller
    {
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
            SupplierGroupSrv _srvGroup = new SupplierGroupSrv();
            Dictionary<string, object> _return = _srvGroup.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<SupplierGroupModel> itemResponse = _return["data"] as JqueryDataTableResponse<SupplierGroupModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<SupplierGroupModel>(), JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Add()
        {
            SupplierGroupModel model = new SupplierGroupModel();
            model.Insert = true;
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            SupplierGroupSrv _srvGroup = new SupplierGroupSrv();
            SupplierGroupModel model = _srvGroup.Item(new Guid(id));
            model.Insert = false;
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpPost]
        public JsonResult Save(SupplierGroupModel model)
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
            SupplierGroupSrv _srvGroup = new SupplierGroupSrv();
            return this.Json(_srvGroup.Save(model), JsonRequestBehavior.AllowGet);
        }

    }
}