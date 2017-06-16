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

namespace Web.Areas.Warehouse.Controllers
{
    public class StockInController : Controller
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
            StockInSrv _srvStockIn = new StockInSrv();
            Dictionary<string, object> _return = _srvStockIn.List(requestData);
            if ((DatabaseExecute)_return["status"] == DatabaseExecute.Success)
            {
                JqueryDataTableResponse<StockInModel> itemResponse = _return["data"] as JqueryDataTableResponse<StockInModel>;
                return this.Json(itemResponse, JsonRequestBehavior.AllowGet);
            }
            return this.Json(new JqueryDataTableResponse<StockInModel>(), JsonRequestBehavior.AllowGet);
        }


        [AjaxAuthorize]
        [HttpGet]
        public ActionResult Add()
        {
            Model.User.UserLoginModel user = Session["user"] as Model.User.UserLoginModel;

            BranchSrv _srvBranch = new BranchSrv();
            var _lBranch = _srvBranch.GetListForDisplay();
            List<SelectListItem> branch = new List<SelectListItem>();
            branch.Add(new SelectListItem() { Value = "", Text = "Chọn chi nhánh" });
            foreach (var item in _lBranch)
            {
                branch.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }ViewBag.branch = branch;
            //
            SupplierSrv _srvSupplier = new SupplierSrv();
            var _lSupplier = _srvSupplier.GetListForDisplay();
            List<SelectListItem> supplier = new List<SelectListItem>();
            supplier.Add(new SelectListItem() { Value = "", Text = "Chọn nhà cung cấp" });
            foreach (var item in _lSupplier)
            {
                supplier.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            ViewBag.supplier = supplier;
            //
            EmployeeSrv _srvEmpl = new EmployeeSrv();
            var _lEmpl = _srvEmpl.GetListForDisplay();
            List<SelectListItem> empl = new List<SelectListItem>();
            foreach (var item in _lEmpl)
            {
                if (item.ID == user.ID)
                {
                    empl.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name, Selected = true });
                }
                else
                {
                    empl.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
                }
            }
            ViewBag.employee = empl;

            //GoodsGroupSrv _srvGroup = new GoodsGroupSrv();
            //var _lGroup = _srvGroup.GetListForDisplay();
            //List<SelectListItem> group = new List<SelectListItem>();
            //group.Add(new SelectListItem() { Value = "", Text = "Chọn nhóm hàng hóa" });
            //foreach (var item in _lGroup)
            //{
            //    group.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            //}
            //ViewBag.group = group;
            //GoodsModel model = new GoodsModel()
            //{
            //    Code = GoodsSrv.GetCode(),
            //    Insert = true,
            //    Avatar = "",
            //    ImageFileName = "",
            //    NumInStock = 0,
            //    MaxInStock = 0,
            //    MinInStock = 0,
            //    Weight = 0
            //};
            //
            StockInModel model = new StockInModel();
            model.Insert = true;
            model.Code = StockInSrv.GetCode();
            model.StockInDate = DateTime.Now;
            model.EmployeeID = user.ID;
            model.IsFinish = false;
            model.CreateBy = user.ID;

            StockInSrv _srvStockin = new StockInSrv();
           //_srvStockin.Save(model);

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
        public JsonResult Save(StockInModel model, FormCollection fc)
        {
            Model.User.UserLoginModel user = Session["user"] as Model.User.UserLoginModel;
            if (model.Insert)
            {
                model.CreateBy = user.ID;
            }
            else
            {
                model.UpdatedBy = user.ID;
            }

            if(fc["goodsId"] != null && fc["goodsId"].ToString().Length > 0)
            {
                string[] goodsIDs = fc["goodsId"].ToString().Split(',');
                string[] orgPrices = fc["goodsOrgPriceValue"].ToString().Split(',');
                string[] discounts = fc["goodsDiscountValue"].ToString().Split(',');
                string[] numbers = fc["goodsNumberValue"].ToString().Split(',');
                string[] totals = fc["goodsTotal"].ToString().Split(',');
                for (int i = 0; i < goodsIDs.Count(); i++)
                {
                    model.details.Add(new StockInDetailModel()
                    {
                        ID = new Guid(goodsIDs[i]),
                        Price = decimal.Parse(orgPrices[i]),
                        Number = decimal.Parse(numbers[i]),
                        Discount = decimal.Parse(discounts[i]),
                        Total = decimal.Parse(totals[i])
                    });
                }
            }
            StockInSrv _srvStockIn = new StockInSrv();
            return this.Json(_srvStockIn.Save(model), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Find location by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [AjaxAuthorize]
        [HttpPost]
        public JsonResult FindGoods(string name)
        {
            GoodsSrv _srvGoods = new GoodsSrv();
            return this.Json(_srvGoods.FindGood(name.ToLower()), JsonRequestBehavior.AllowGet);
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