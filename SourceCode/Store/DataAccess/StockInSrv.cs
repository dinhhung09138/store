using Common;
using Common.JqueryDataTable;
using Common.Message;
using Common.Status;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class StockInSrv
    {
        /// <summary>
        /// Get list item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Dictionary<string, object> List(CustomJqueryDataTableRequest request)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                //Declare response data to json object
                JqueryDataTableResponse<StockInModel> itemResponse = new JqueryDataTableResponse<StockInModel>();
                //List of data
                List<StockInModel> _list = new List<StockInModel>();
                using (var context = new StoreEntities())
                {
                    var l = (from a in context.stock_in
                             join e in context.employees on a.empl_id equals e.id
                             join br in context.branches on a.branch_id equals br.id into branch
                             join s in context.suppliers on a.supplier_id equals s.id
                             from b in branch.DefaultIfEmpty()
                             where !a.deleted
                             orderby a.code
                             select new
                             {
                                 a.id,
                                 a.code,
                                 a.stock_in_date,
                                 a.total_money,
                                 a.dept,
                                 employee_name = e.name,
                                 branch_name = b.name,
                                 supplier_name = s.name
                             }).ToList();
                    itemResponse.draw = request.draw;
                    itemResponse.recordsTotal = l.Count;
                    //Search
                    if (!string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        l = l.Where(m => m.code.ToLower().Contains(searchValue) ||
                                    m.employee_name.ToLower().Contains(searchValue) ||
                                    m.branch_name.ToLower().Contains(searchValue) ||
                                    m.supplier_name.ToLower().Contains(searchValue) ||
                                    m.stock_in_date.ToString().ToLower().Contains(searchValue) ||
                                    m.total_money.ToString().Contains(searchValue) ||
                                    m.dept.ToString().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in l)
                    {
                        _list.Add(new StockInModel()
                        {
                            ID = item.id,
                            Code = item.code,
                            EmployeeName = item.employee_name,
                            BranchName = item.branch_name,
                            StockInDate = item.stock_in_date,
                            StockInDateString = item.stock_in_date.ToShortDateString(),
                            TotalMoney = item.total_money,
                            Dept = item.dept,
                            SupplierName = item.supplier_name
                        });
                    }
                    itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<StockInModel> _sortList = null;
                    foreach (var col in request.order)
                    {
                        switch (col.ColumnName)
                        {
                            case "Code":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Code) : _sortList.Sort(col.Dir, m => m.Code);
                                break;
                            case "EmployeeName":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.EmployeeName) : _sortList.Sort(col.Dir, m => m.EmployeeName);
                                break;
                            case "BranchName":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.BranchName) : _sortList.Sort(col.Dir, m => m.BranchName);
                                break;
                            case "StockInDateString":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.StockInDateString) : _sortList.Sort(col.Dir, m => m.StockInDateString);
                                break;
                            case "TotalMoney":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.TotalMoney) : _sortList.Sort(col.Dir, m => m.TotalMoney);
                                break;
                            case "Dept":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Dept) : _sortList.Sort(col.Dir, m => m.Dept);
                                break;
                            case "SupplierName":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.SupplierName) : _sortList.Sort(col.Dir, m => m.SupplierName);
                                break;
                        }
                    }
                    itemResponse.data = _sortList.Skip(request.start).Take(request.length).ToList();
                    _return.Add("data", itemResponse);
                }
                _return.Add("status", DatabaseExecute.Success);
            }
            catch (Exception ex)
            {
                _return.Add("status", DatabaseExecute.Error);
                _return.Add("systemMessage", ex.Message);
                _return.Add("message", DatabaseMessage.LIST_ERROR);
            }

            return _return;
        }

        /// <summary>
        /// Return dynamic item code
        /// </summary>
        /// <returns></returns>
        public static string GetCode()
        {
            try
            {
                using (var context = new StoreEntities())
                {
                    int count = context.stock_in.Count();
                    return Utils.STOCKIN_CODE + count.ReturnTo9Digit();
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="id">id of item</param>
        /// <returns></returns>
        public StockInModel Item(Guid id)
        {
            StockInModel _item = new StockInModel();
            try
            {
                using (var context = new StoreEntities())
                {
                    var item = (from m in context.stock_in
                                join b in context.branches on m.branch_id equals b.id
                                join s in context.suppliers on m.supplier_id equals s.id
                                join e in context.employees on m.empl_id equals e.id
                                where m.id == id
                                select new
                                {
                                    m.id,
                                    m.code,
                                    m.branch_id,
                                    branch_name = b.name,
                                    m.stock_in_date,
                                    m.total_money,
                                    m.supplier_id,
                                    supplier_name = s.name,
                                    m.discount,
                                    m.payable,
                                    m.dept,
                                    m.empl_id,
                                    empl_name = e.name,
                                    m.reason,
                                    m.is_finish,
                                    m.notes
                                }).First();
                    _item.ID = item.id;
                    _item.Code = item.code;
                    _item.BranchID = item.branch_id;
                    _item.BranchName = item.branch_name;
                    _item.StockInDate = item.stock_in_date;
                    _item.StockInDateString = item.stock_in_date.ToString();
                    _item.SupplierID = item.supplier_id;
                    _item.SupplierName = item.supplier_name;
                    _item.TotalMoney = item.total_money;
                    _item.Discount = item.discount;
                    _item.Payable = item.payable;
                    _item.Dept = item.dept;
                    _item.EmployeeID = item.empl_id;
                    _item.EmployeeName = item.empl_name;
                    _item.Reason = item.reason;
                    _item.Notes = item.notes;
                    _item.IsFinish = item.is_finish;
                    _item.Insert = false;

                    var details = (from m in context.stock_in_detail
                                   join p in context.goods on m.goods_id equals p.id
                                   where m.stock_in_id == item.id
                                   select new
                                   {
                                       m.id,
                                       m.goods_id,
                                       goods_name = p.name,
                                       m.number,
                                       m.price,
                                       m.discount,
                                   }).ToList();
                    foreach (var it in details)
                    {
                        _item.details.Add(new StockInDetailModel()
                        {
                            ID = it.id,
                            GoodsID = it.goods_id,
                            GoodsName = it.goods_name,
                            Number = it.number,
                            Price = it.price,
                            Discount = it.discount,
                            Total = it.price * it.number - it.discount
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                //_return.Add("status", DatabaseExecute.Error);
                //_return.Add("systemMessage", ex.Message);
                //_return.Add("message", DatabaseMessage.ITEM_ERROR);
            }

            return _item;
        }

        /// <summary>
        /// Save item
        /// </summary>
        /// <param name="model">Motel</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, object> Save(StockInModel model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    stock_in md = new stock_in();
                    using (var trans = context.Database.BeginTransaction())
                    {
                        if (model.Insert)
                        {
                            md.id = Guid.NewGuid();
                            md.code = model.Code;
                            md.empl_id = model.EmployeeID;
                            md.stock_in_date = model.StockInDate;
                            md.branch_id = model.BranchID;
                            md.supplier_id = model.SupplierID;
                            md.total_money = model.TotalMoney;
                            md.discount = model.Discount;
                            md.payable = model.Payable;
                            md.dept = model.Dept;
                            md.reason = model.Reason;
                            md.notes = model.Notes;
                            md.is_finish = model.IsFinish;
                            md.create_by = model.CreateBy;
                            md.create_date = DateTime.Now;
                            md.deleted = false;
                            context.stock_in.Add(md);
                            context.Entry(md).State = System.Data.Entity.EntityState.Added;
                            context.SaveChanges();
                            foreach (var item in model.details)
                            {
                                stock_in_detail dt = new stock_in_detail();
                                dt.id = Guid.NewGuid();
                                dt.stock_in_id = md.id;
                                dt.goods_id = item.GoodsID;
                                dt.number = item.Number;
                                dt.price = item.Price;
                                dt.discount = item.Discount;
                                context.stock_in_detail.Add(dt);
                                context.Entry(dt).State = System.Data.Entity.EntityState.Added;
                            }
                        }
                        else
                        {
                            md = context.stock_in.FirstOrDefault(m => m.id == model.ID);
                            md.code = model.Code;
                            md.empl_id = model.EmployeeID;
                            md.stock_in_date = model.StockInDate;
                            md.branch_id = model.BranchID;
                            md.supplier_id = model.SupplierID;
                            md.total_money = model.TotalMoney;
                            md.discount = model.Discount;
                            md.payable = model.Payable;
                            md.dept = model.Dept;
                            md.empl_id = model.EmployeeID;
                            md.reason = model.Reason;
                            md.notes = model.Notes;
                            md.is_finish = model.IsFinish;
                            md.update_by = model.UpdatedBy;
                            md.update_date = DateTime.Now;
                            context.stock_in.Attach(md);
                            context.Entry(md).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            //
                            var listDt = context.stock_in_detail.Where(m => m.stock_in_id == md.id).ToList();
                            context.stock_in_detail.RemoveRange(listDt);
                            //context.Entry(listDt).State = System.Data.Entity.EntityState.Deleted;
                            //
                            foreach (var item in model.details)
                            {
                                //
                                stock_in_detail dt = new stock_in_detail();
                                dt.id = Guid.NewGuid();
                                dt.stock_in_id = md.id;
                                dt.goods_id = item.GoodsID;
                                dt.number = item.Number;
                                dt.price = item.Price;
                                dt.discount = item.Discount;
                                context.stock_in_detail.Add(dt);
                                context.Entry(dt).State = System.Data.Entity.EntityState.Added;
                            }
                        }
                        context.SaveChanges();
                        trans.Commit();
                    }
                    
                }
                _return.Add("status", DatabaseExecute.Success);
                _return.Add("message", DatabaseMessage.SAVE_SUCCESS);
            }
            catch (Exception ex)
            {
                _return.Add("status", DatabaseExecute.Error);
                _return.Add("systemMessage", ex.Message);
                _return.Add("message", DatabaseMessage.SAVE_ERROR);
            }

            return _return;
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id">id of item</param>
        /// <param name="userID">User deleted item</param>
        /// <returns></returns>
        public Dictionary<string, object> Delete(Guid id, Guid userID)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    var md = context.stock_in.First(m => m.id == id);
                    md.deleted = true;
                    md.delete_by = userID;
                    md.delete_date = DateTime.Now;
                    context.stock_in.Attach(md);
                    context.Entry(md).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                _return.Add("status", DatabaseExecute.Success);
                _return.Add("message", DatabaseMessage.DELETE_SUCCESS);
            }
            catch (Exception ex)
            {
                _return.Add("status", DatabaseExecute.Error);
                _return.Add("systemMessage", ex.Message);
                _return.Add("message", DatabaseMessage.DELETE_ERROR);
            }

            return _return;
        }
    }
}
