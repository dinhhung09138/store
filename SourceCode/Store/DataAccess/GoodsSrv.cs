using Common;
using Common.JqueryDataTable;
using Common.Message;
using Common.Status;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GoodsSrv
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
                JqueryDataTableResponse<GoodsModel> itemResponse = new JqueryDataTableResponse<GoodsModel>();
                //List of data
                List<GoodsModel> _list = new List<GoodsModel>();
                using (var context = new StoreEntities())
                {
                    var l = (from a in context.goods
                             join g in context.goods_group on a.group_id equals g.id into grour_cus
                             from g1 in grour_cus.DefaultIfEmpty()
                             join loc in context.units on a.unit_id equals loc.id into lc_unit
                             from l1 in lc_unit.DefaultIfEmpty()
                             where !a.deleted
                             orderby a.name
                             select new
                             {
                                 a.id,
                                 a.code,
                                 a.name,
                                 org_price = a.org_price ?? 0,
                                 price = a.price ?? 0,
                                 number_in_stock = a.number_in_stock ?? 0,
                                 group_name = g1.name,
                                 unit_name = l1.name
                             }).ToList();

                    itemResponse.draw = request.draw;
                    itemResponse.recordsTotal = l.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        l = l.Where(m => m.name.ToLower().Contains(searchValue) ||
                                    m.code.ToLower().Contains(searchValue) ||
                                    m.price.ToString().Contains(searchValue) ||
                                    m.org_price.ToString().Contains(searchValue) ||
                                    m.number_in_stock.ToString().Contains(searchValue) ||
                                    m.group_name.ToLower().Contains(searchValue) ||
                                    m.unit_name.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in l)
                    {
                        _list.Add(new GoodsModel()
                        {
                            ID = item.id,
                            Code = item.code,
                            Name = item.name,
                            Price = item.price,
                            OrgPrice = item.org_price,
                            NumInStock = item.number_in_stock,
                            GroupName = item.group_name,
                            UnitName = item.unit_name
                        });
                    }
                    itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<GoodsModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Code":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Code) : _sortList.Sort(col.Dir, m => m.Code);
                                    break;
                                case "Name":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
                                    break;
                                case "Price":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Price) : _sortList.Sort(col.Dir, m => m.Price);
                                    break;
                                case "OrgPrice":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.OrgPrice) : _sortList.Sort(col.Dir, m => m.OrgPrice);
                                    break;
                                case "NumInStock":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.NumInStock) : _sortList.Sort(col.Dir, m => m.NumInStock);
                                    break;
                                case "GroupName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.GroupName) : _sortList.Sort(col.Dir, m => m.GroupName);
                                    break;
                                case "UnitName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.UnitName) : _sortList.Sort(col.Dir, m => m.UnitName);
                                    break;
                            }
                        }
                        itemResponse.data = _sortList.Skip(request.start).Take(request.length).ToList();
                    }
                    else
                    {
                        itemResponse.data = _list.Skip(request.start).Take(request.length).ToList();
                    }
                    _return.Add("data", itemResponse);
                }
                _return.Add("status", DatabaseExecute.Success);
            }
            catch (Exception ex)
            {
                //_return.Add("status", DatabaseExecute.Error);
                //_return.Add("systemMessage", ex.Message);
                //_return.Add("message", DatabaseMessage.LIST_ERROR);
                //Common.Logs.AddLog("Customersvr/List", "", ex.StackTrace, ex.Message);
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
                    int count = context.goods.Count();
                    return Utils.GOODS_CODE + count.ReturnTo9Digit();
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
        public GoodsModel Item(Guid id)
        {
            GoodsModel _item = new GoodsModel() { ID = Guid.NewGuid() };
            try
            {

                using (var context = new StoreEntities())
                {
                    var item = (from m in context.goods
                                join g in context.goods_group on m.group_id equals g.id into grour_cus
                                from g1 in grour_cus.DefaultIfEmpty()
                                join u in context.units on m.unit_id equals u.id into lc_cus
                                from l1 in lc_cus.DefaultIfEmpty()
                                where m.id == id
                                select new
                                {
                                    m.id,
                                    m.code,
                                    m.name,
                                    m.avatar,
                                    m.unit_id,
                                    UnitName = l1.name,
                                    m.group_id,
                                    GroupName = g1.name,
                                    m.price,
                                    m.org_price,
                                    m.sales_directive,
                                    m.weight,
                                    m.description,
                                    m.number_in_stock,
                                    m.min_in_stock,
                                    m.max_in_stock,
                                    m.note_in_order
                                }).First();
                    _item.ID = item.id;
                    _item.Code = item.code;
                    _item.Name = item.name;
                    _item.UnitID = item.unit_id;
                    _item.UnitName = item.UnitName;
                    _item.GroupID = item.group_id;
                    _item.GroupName = item.GroupName;
                    _item.Avatar = item.avatar;
                    _item.ImageFileName = "";
                    _item.Price = item.price ?? 0;
                    _item.OrgPrice = item.org_price ?? 0;
                    _item.Weight = item.weight;
                    _item.Description = item.description;
                    _item.AllowSaleDirect = item.sales_directive;
                    _item.NumInStock = item.number_in_stock ?? 0;
                    _item.MinInStock = item.min_in_stock ?? 0;
                    _item.MaxInStock = item.max_in_stock ?? 0;
                    _item.NoteInOrder = item.note_in_order;
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
        public Dictionary<string, object> Save(GoodsModel model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    good md = new good();
                    if (model.Insert)
                    {
                        md.id = Guid.NewGuid();
                        md.name = model.Name;
                        md.code = model.Code;
                        md.unit_id = model.UnitID;
                        md.group_id = model.GroupID;
                        md.price = model.Price;
                        md.org_price = model.OrgPrice;
                        md.number_in_stock = model.NumInStock;
                        md.sales_directive = model.AllowSaleDirect;
                        md.avatar = model.Avatar;
                        md.weight = model.Weight;
                        md.description = model.Description;
                        md.min_in_stock = model.MinInStock;
                        md.max_in_stock = model.MaxInStock;
                        md.note_in_order = model.NoteInOrder;
                        md.create_by = model.CreateBy;
                        md.create_date = DateTime.Now;
                        md.deleted = false;
                        context.goods.Add(md);
                        context.Entry(md).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        md = context.goods.FirstOrDefault(m => m.id == model.ID);
                        md.name = model.Name;
                        md.code = model.Code;
                        md.unit_id = model.UnitID;
                        md.group_id = model.GroupID;
                        md.price = model.Price;
                        md.org_price = model.OrgPrice;
                        md.number_in_stock = model.NumInStock;
                        md.avatar = model.Avatar;
                        md.weight = model.Weight;
                        md.sales_directive = model.AllowSaleDirect;
                        md.description = model.Description;
                        md.min_in_stock = model.MinInStock;
                        md.max_in_stock = model.MaxInStock;
                        md.note_in_order = model.NoteInOrder;
                        md.create_by = model.CreateBy;
                        md.create_date = DateTime.Now;
                        md.update_by = model.UpdatedBy;
                        md.update_date = DateTime.Now;
                        context.goods.Attach(md);
                        context.Entry(md).State = System.Data.Entity.EntityState.Modified;
                    }
                    context.SaveChanges();
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
                    var md = context.goods.First(m => m.id == id);
                    md.deleted = true;
                    md.delete_by = userID;
                    md.delete_date = DateTime.Now;
                    context.goods.Attach(md);
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

        public List<GoodsModel> FindGood(string searchString)
        {
            List<GoodsModel> _return = new List<GoodsModel>();

            using (var context = new StoreEntities())
            {
                var list = (from a in context.goods
                            where a.code.Contains(searchString) || a.name.Contains(searchString)
                            select new
                            {
                                a.id,
                                a.code,
                                a.name,
                                a.org_price
                            }).ToList();
                foreach (var item in list)
                {
                    _return.Add(new GoodsModel()
                    {
                        ID = item.id,
                        Code = item.code,
                        Name  = item.name,
                        OrgPrice = item.org_price ?? 0
                    });
                }
            }

            return _return;

        }

    }
}
