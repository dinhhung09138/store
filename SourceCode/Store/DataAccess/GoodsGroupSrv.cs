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
    /// <summary>
    /// Goods's group service
    /// </summary>
    public class GoodsGroupSrv
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
                JqueryDataTableResponse<GoodsGroupModel> itemResponse = new JqueryDataTableResponse<GoodsGroupModel>();
                //List of data
                List<GoodsGroupModel> _list = new List<GoodsGroupModel>();
                using (var context = new StoreEntities())
                {
                    var l = (from a in context.goods_group where !a.deleted orderby a.name select new { a.id, a.name, a.notes }).ToList();
                    itemResponse.draw = request.draw;
                    itemResponse.recordsTotal = l.Count;
                    //Search
                    if (!string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        l = l.Where(m => m.name.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in l)
                    {
                        _list.Add(new GoodsGroupModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            Notes = item.notes
                        });
                    }
                    itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<GoodsGroupModel> _sortList = null;
                    foreach (var col in request.order)
                    {
                        switch (col.ColumnName)
                        {
                            case "Name":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
                                break;
                            case "Notes":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Notes) : _sortList.Sort(col.Dir, m => m.Notes);
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
        /// Get item
        /// </summary>
        /// <param name="id">id of item</param>
        /// <returns></returns>
        public GoodsGroupModel Item(Guid id)
        {
            GoodsGroupModel _item = new GoodsGroupModel() { ID = Guid.NewGuid() };
            try
            {
                using (var context = new StoreEntities())
                {
                    var item = context.goods_group.First(m => m.id == id);
                    _item.ID = item.id;
                    _item.Name = item.name;
                    _item.Notes = item.notes;
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
        public Dictionary<string, object> Save(GoodsGroupModel model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    goods_group md = new goods_group();
                    if (model.Insert)
                    {
                        md.id = Guid.NewGuid();
                        md.name = model.Name;
                        md.notes = model.Notes;
                        md.create_by = model.CreateBy;
                        md.create_date = DateTime.Now;
                        md.deleted = false;
                        context.goods_group.Add(md);
                        context.Entry(md).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        md = context.goods_group.FirstOrDefault(m => m.id == model.ID);
                        md.name = model.Name;
                        md.notes = model.Notes;
                        md.update_by = model.UpdatedBy;
                        md.update_date = DateTime.Now;
                        context.goods_group.Attach(md);
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
                    var md = context.goods_group.First(m => m.id == id);
                    md.deleted = true;
                    md.delete_by = userID;
                    md.delete_date = DateTime.Now;
                    context.goods_group.Attach(md);
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

        /// <summary>
        /// Get list group of goods to display on dropdown controll (or for something else) in other form
        /// </summary>
        /// <returns></returns>
        public List<GoodsGroupModel> GetListForDisplay()
        {
            List<GoodsGroupModel> _return = new List<GoodsGroupModel>();
            using (var context = new StoreEntities())
            {
                var list = (from a in context.goods_group where !a.deleted orderby a.name select new { a.id, a.name }).ToList();
                foreach (var item in list)
                {
                    _return.Add(new GoodsGroupModel() { ID = item.id, Name = item.name });
                }
            }
            return _return;
        }

    }
}
