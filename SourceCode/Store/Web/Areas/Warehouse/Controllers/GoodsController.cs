﻿using Common;
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

namespace Web.Areas.Warehouse.Controllers
{
    public class GoodsController : Controller
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
            GoodsSrv _srvGoods = new GoodsSrv();
            Dictionary<string, object> _return = _srvGoods.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<GoodsModel> itemResponse = _return["data"] as JqueryDataTableResponse<GoodsModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<GoodsModel>(), JsonRequestBehavior.AllowGet);
        }


        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Add()
        {
            UnitSrv _srvUnit = new UnitSrv();
            var _lUnit = _srvUnit.GetListForDisplay();
            List<SelectListItem> groupUnit = new List<SelectListItem>();
            groupUnit.Add(new SelectListItem() { Value = "", Text = "Chọn đơn vị tính" });
            foreach (var item in _lUnit)
            {
                groupUnit.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            ViewBag.unit = groupUnit;

            GoodsGroupSrv _srvGroup = new GoodsGroupSrv();
            var _lGroup = _srvGroup.GetListForDisplay();
            List<SelectListItem> group = new List<SelectListItem>();
            group.Add(new SelectListItem() { Value = "", Text = "Chọn nhóm hàng hóa" });
            foreach (var item in _lGroup)
            {
                group.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            ViewBag.group = group;
            GoodsModel model = new GoodsModel()
            {
                Code = GoodsSrv.GetCode(),
                Insert = true,
                Avatar = "",
                ImageFileName = "",
                NumInStock = 0,
                MaxInStock = 0,
                MinInStock = 0,
                Weight = 0
            };
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            UnitSrv _srvUnit = new UnitSrv();
            var _lUnit = _srvUnit.GetListForDisplay();
            List<SelectListItem> groupUnit = new List<SelectListItem>();
            groupUnit.Add(new SelectListItem() { Value = "", Text = "Chọn đơn vị tính" });
            foreach (var item in _lUnit)
            {
                groupUnit.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            ViewBag.unit = groupUnit;

            GoodsGroupSrv _srvGroup = new GoodsGroupSrv();
            var _lGroup = _srvGroup.GetListForDisplay();
            List<SelectListItem> group = new List<SelectListItem>();
            group.Add(new SelectListItem() { Value = "", Text = "Chọn nhóm hàng hóa" });
            foreach (var item in _lGroup)
            {
                group.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            ViewBag.group = group;

            GoodsSrv _serGoods = new GoodsSrv();
            GoodsModel model = _serGoods.Item(new Guid(id));
            model.Insert = false;
            return PartialView(model);
        }

        [AjaxAuthorize]
        [HttpPost]
        public JsonResult Save(GoodsModel model, FormCollection fc)
        {
            Model.User.UserLoginModel user = Session["user"] as Model.User.UserLoginModel;
            if(fc["Price"] != null && fc["Price"].ToString().Length > 0)
            {
                model.Price = decimal.Parse(fc["Price"].ToString().Replace(",", ""));
            }
            if (fc["OrgPrice"] != null && fc["OrgPrice"].ToString().Length > 0)
            {
                model.OrgPrice = decimal.Parse(fc["OrgPrice"].ToString().Replace(",", ""));
            }
            if (fc["Weight"] != null && fc["Weight"].ToString().Length > 0)
            {
                model.Weight = decimal.Parse(fc["Weight"].ToString().Replace(",", ""));
            }
            if (fc["NumInStock"] != null && fc["NumInStock"].ToString().Length > 0)
            {
                model.NumInStock = decimal.Parse(fc["NumInStock"].ToString().Replace(",", ""));
            }
            if (fc["MaxInStock"] != null && fc["MaxInStock"].ToString().Length > 0)
            {
                model.MaxInStock = decimal.Parse(fc["MaxInStock"].ToString().Replace(",", ""));
            }
            if (fc["MinInStock"] != null && fc["MinInStock"].ToString().Length > 0)
            {
                model.MinInStock = decimal.Parse(fc["MinInStock"].ToString().Replace(",", ""));
            }
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
                if (SaveAvatar(file, Server.MapPath("~/Files/Goods/" + model.ID + "." + extension)))
                {
                    model.Avatar = "/Files/Goods/" + model.ID + "." + extension;
                }
            }
            GoodsSrv _srvGoods = new GoodsSrv();
            return this.Json(_srvGoods.Save(model), JsonRequestBehavior.AllowGet);
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