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

namespace Web.Areas.Warehouse.Controllers
{
    public class UnitController : Controller
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
            UnitSrv _srvUnit = new UnitSrv();
            Dictionary<string, object> _return = _srvUnit.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<UnitModel> itemResponse = _return["data"] as JqueryDataTableResponse<UnitModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<UnitModel>(), JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Add()
        {
            UnitModel model = new UnitModel();
            model.Insert = true;
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            UnitSrv _srvUnit = new UnitSrv();
            UnitModel model = _srvUnit.Item(new Guid(id));
            model.Insert = false;
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpPost]
        public JsonResult Save(UnitModel model)
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
            UnitSrv _srvUnit = new UnitSrv();
            return this.Json(_srvUnit.Save(model), JsonRequestBehavior.AllowGet);
        }

    }
}