using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Web.Filters;
using Common;
using Common.JqueryDataTable;
using Common.Status;
using DataAccess;
using System.Drawing;

namespace Web.Areas.Internal.Controllers
{
    public class EmployeeController : Controller
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
            EmployeeSrv _srvEmpl = new EmployeeSrv();
            Dictionary<string, object> _return = _srvEmpl.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<EmployeeModel> itemResponse = _return["data"] as JqueryDataTableResponse<EmployeeModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<EmployeeModel>(), JsonRequestBehavior.AllowGet);
        }
        
        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Add()
        {
            ContractTypeSrv _srvContract = new ContractTypeSrv();
            var _lGroup = _srvContract.GetListForDisplay();
            List<SelectListItem> group = new List<SelectListItem>();
            group.Add(new SelectListItem() { Value = "", Text = "Chọn loại hợp đồng" });
            foreach (var item in _lGroup)
            {
                group.Add(new SelectListItem() { Value = item.Code.ToString(), Text = item.Name });
            }
            ViewBag.group = group;
            EmployeeModel model = new EmployeeModel()
            {
                Code = EmployeeSrv.GetCode(),
                Insert = true,
                Gender = true,
                Avatar = "",
                ImageFileName = ""
            };
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            ContractTypeSrv _srvContract = new ContractTypeSrv();
            var _lGroup = _srvContract.GetListForDisplay();
            List<SelectListItem> group = new List<SelectListItem>();
            group.Add(new SelectListItem() { Value = "", Text = "Chọn loại hợp đồng" });
            foreach (var item in _lGroup)
            {
                group.Add(new SelectListItem() { Value = item.Code.ToString(), Text = item.Name });
            }
            ViewBag.group = group;

            EmployeeSrv _serEmpl = new EmployeeSrv();
            EmployeeModel model = _serEmpl.Item(new Guid(id));
            model.Insert = false;
            return PartialView(model);
        }
        
        [AjaxAuthorize]
        [HttpPost]
        public JsonResult Save(EmployeeModel model)
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
            if (model.ImageFileName != null && model.ImageFileName.Length > 0)
            {
                string extension = model.ImageFileName.Substring(model.ImageFileName.LastIndexOf('.') + 1);
                byte[] file = model.Avatar.ConvertBase64ToByte(extension);
                model.Avatar = "";
                if (SaveAvatar(file, Server.MapPath("~/Files/Employee/" + model.ID + "." + extension)))
                {
                    model.Avatar = "/Files/Employee/" + model.ID + "." + extension;
                }
            }
            EmployeeSrv _serEmpl = new EmployeeSrv();
            return this.Json(_serEmpl.Save(model), JsonRequestBehavior.AllowGet);
        }

        private bool SaveAvatar(byte[] file, string fileName)
        {
            try
            {
                using (var ms = new MemoryStream(file, 0, file.Length))
                {
                    Image image = Image.FromStream(ms, true);
                    image.Save(fileName);
                    return true;
                }
            }
            catch (Exception ex) { return false; }

        }
    }
}