using Common;
using Common.JqueryDataTable;
using Common.Status;
using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Areas.Partner.Controllers
{
    public class SupplierController : Controller
    {
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
            SupplierSrv _srvSupplier = new SupplierSrv();
            Dictionary<string, object> _return = _srvSupplier.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<SupplierModel> itemResponse = _return["data"] as JqueryDataTableResponse<SupplierModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<SupplierModel>(), JsonRequestBehavior.AllowGet);
        }


        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Add()
        {
            SupplierGroupSrv _srvGroup = new SupplierGroupSrv();
            var _lGroup = _srvGroup.GetListForDisplay();
            List<SelectListItem> group = new List<SelectListItem>();
            group.Add(new SelectListItem() { Value = "", Text = "Chọn nhóm đối tác" });
            foreach (var item in _lGroup)
            {
                group.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            ViewBag.group = group;
            SupplierModel model = new SupplierModel()
            {
                Insert = true,
                Avatar = "",
                ImageFileName = ""
            };
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            SupplierGroupSrv _srvGroup = new SupplierGroupSrv();
            var _lGroup = _srvGroup.GetListForDisplay();
            List<SelectListItem> group = new List<SelectListItem>();
            group.Add(new SelectListItem() { Value = "", Text = "Chọn nhóm đối tác" });
            foreach (var item in _lGroup)
            {
                group.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            ViewBag.group = group;

            SupplierSrv _srvSupplier = new SupplierSrv();
            SupplierModel model = _srvSupplier.Item(new Guid(id));
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
        public JsonResult Save(SupplierModel model)
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
                if (SaveAvatar(file, Server.MapPath("~/Files/Supplier/" + model.ID + "." + extension)))
                {
                    model.Avatar = "/Files/Supplier/" + model.ID + "." + extension;
                }
            }
            SupplierSrv _srvSupplier = new SupplierSrv();
            return this.Json(_srvSupplier.Save(model), JsonRequestBehavior.AllowGet);
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