using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;
using DataAccess;
using Common.Status;
using Common.JqueryDataTable;
using Model;
using System.IO;
using System.Drawing;
using Common;

namespace Web.Areas.Partner.Controllers
{
    public class CustomerController : Controller
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
            CustomerSrv _srvCustomer = new CustomerSrv();
            Dictionary<string, object> _return = _srvCustomer.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<CustomerModel> itemResponse = _return["data"] as JqueryDataTableResponse<CustomerModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<CustomerModel>(), JsonRequestBehavior.AllowGet);
        }


        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Add()
        {
            CustomerGroupSrv _srvGroup = new CustomerGroupSrv();
            var _lGroup = _srvGroup.GetListForDisplay();
            List<SelectListItem> group = new List<SelectListItem>();
            group.Add(new SelectListItem() { Value = "", Text = "Chọn nhóm khách hàng" });
            foreach (var item in _lGroup)
            {
                group.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            ViewBag.group = group;
            CustomerModel model = new CustomerModel()
            {
                Insert = true,
                IsCompany = false,
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
            CustomerGroupSrv _srvGroup = new CustomerGroupSrv();
            var _lGroup = _srvGroup.GetListForDisplay();
            List<SelectListItem> group = new List<SelectListItem>();
            group.Add(new SelectListItem() { Value = "", Text = "Chọn nhóm khách hàng" });
            foreach (var item in _lGroup)
            {
                group.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            ViewBag.group = group;

            CustomerSrv _serCustomer = new CustomerSrv();
            CustomerModel model = _serCustomer.Item(new Guid(id));
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
        public JsonResult Save(CustomerModel model)
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
                if (SaveAvatar(file, Server.MapPath("~/Files/Customer/" + model.ID + "." + extension)))
                {
                    model.Avatar = "/Files/Customer/" + model.ID + "." + extension;
                }
            }
            CustomerSrv _srvCustomer = new CustomerSrv();
            return this.Json(_srvCustomer.Save(model), JsonRequestBehavior.AllowGet);
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
            catch(Exception ex) { return false; }

        }
    }
}