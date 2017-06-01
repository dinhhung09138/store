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
    public class CustomerGroupController : Controller
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
            CustomerGroupSrv _srvGroup = new CustomerGroupSrv();
            Dictionary<string, object> _return = _srvGroup.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<CustomerGroupModel> itemResponse = _return["data"] as JqueryDataTableResponse<CustomerGroupModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<CustomerGroupModel>(), JsonRequestBehavior.AllowGet);
        }
        
        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Add()
        {
            CustomerGroupModel model = new CustomerGroupModel();
            model.Insert = true;
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            CustomerGroupSrv _srvGroup = new CustomerGroupSrv();
            CustomerGroupModel model = _srvGroup.Item(new Guid(id));
            model.Insert = false;
            return PartialView(model);
        }
        
        [AjaxAuthorize]
        [HttpPost]
        public JsonResult Save(CustomerGroupModel model)
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
            CustomerGroupSrv _srvGroup = new CustomerGroupSrv();
            return this.Json(_srvGroup.Save(model), JsonRequestBehavior.AllowGet);
        }

    }
}