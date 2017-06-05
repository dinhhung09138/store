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
    public class GoodsGroupController : Controller
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
            GoodsGroupSrv _srvGroup = new GoodsGroupSrv();
            Dictionary<string, object> _return = _srvGroup.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<GoodsGroupModel> itemResponse = _return["data"] as JqueryDataTableResponse<GoodsGroupModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<GoodsGroupModel>(), JsonRequestBehavior.AllowGet);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Add()
        {
            GoodsGroupModel model = new GoodsGroupModel();
            model.Insert = true;
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            GoodsGroupSrv _srvGroup = new GoodsGroupSrv();
            GoodsGroupModel model = _srvGroup.Item(new Guid(id));
            model.Insert = false;
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpPost]
        public JsonResult Save(GoodsGroupModel model)
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
            GoodsGroupSrv _srvGroup = new GoodsGroupSrv();
            return this.Json(_srvGroup.Save(model), JsonRequestBehavior.AllowGet);
        }

    }
}