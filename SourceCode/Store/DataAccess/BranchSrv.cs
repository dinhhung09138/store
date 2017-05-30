using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Common.JqueryDataTable;
using Common.Status;
using Common.Message;

namespace DataAccess
{
    /// <summary>
    /// Branch service
    /// </summary>
    public class BranchSrv
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
                JqueryDataTableResponse<BranchModel> itemResponse = new JqueryDataTableResponse<BranchModel>();
                //List of data
                List<BranchModel> _list = new List<BranchModel>();
                using (var context = new StoreEntities())
                {
                    var l = (from a in context.branches
                             where !a.deleted
                             orderby a.name
                             select new
                             {
                                 a.id,
                                 a.name,
                                 a.address,
                                 a.phone,
                                 a.hotline
                             }).ToList();
                    itemResponse.draw = request.draw;
                    itemResponse.recordsTotal = l.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        l = l.Where(m => m.name.ToLower().Contains(searchValue) ||
                                    m.address.ToLower().Contains(searchValue) ||
                                    m.phone.ToLower().Contains(searchValue) ||
                                    m.hotline.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in l)
                    {
                        _list.Add(new BranchModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            Address = item.address,
                            Phone = item.phone,
                            Hotline = item.hotline
                        });
                    }
                    itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<BranchModel> _sortList = null;
                    if (request.order != null)
                    {
                        foreach (var col in request.order)
                        {
                            switch (col.ColumnName)
                            {
                                case "Name":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
                                    break;
                                case "Phone":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Phone) : _sortList.Sort(col.Dir, m => m.Phone);
                                    break;
                                case "Hotline":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Hotline) : _sortList.Sort(col.Dir, m => m.Hotline);
                                    break;
                                case "Address":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Address) : _sortList.Sort(col.Dir, m => m.Address);
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
            }

            return _return;
        }

        /// <summary>
        /// Get item
        /// </summary>
        /// <param name="id">id of item</param>
        /// <returns></returns>
        public BranchModel Item(Guid id)
        {
            BranchModel _item = new BranchModel() { ID = Guid.NewGuid() };
            try
            {
                using (var context = new StoreEntities())
                {
                    var item = (from m in context.branches
                                join l in context.locations on m.location_id equals l.id into br_location
                                from l1 in br_location.DefaultIfEmpty()
                                where m.id == id
                                select new
                                {
                                    m.id,
                                    m.name,
                                    m.address,
                                    m.phone,
                                    m.hotline,
                                    m.num_of_employee,
                                    m.notes,
                                    m.open_day,
                                    m.location_id,
                                    locationName = l1.name
                                }).First();
                    _item.ID = item.id;
                    _item.Name = item.name;
                    _item.Address = item.address;
                    _item.Phone = item.phone;
                    _item.Hotline = item.hotline;
                    _item.EmployeeNumber = item.num_of_employee;
                    _item.OpenDate = item.open_day;
                    _item.Notes = item.notes;
                    _item.LocationID = item.location_id;
                    _item.LocationName = item.locationName;
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
        public Dictionary<string, object> Save(BranchModel model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    branch md = new branch();
                    if (model.Insert)
                    {
                        md.id = Guid.NewGuid();
                        md.name = model.Name;
                        md.address = model.Address;
                        md.phone = model.Phone;
                        md.hotline = model.Hotline;
                        md.num_of_employee = 0;
                        md.open_day = model.OpenDate;
                        md.notes = model.Notes;
                        md.location_id = model.LocationID;
                        md.create_by = model.CreateBy;
                        md.create_date = DateTime.Now;
                        md.deleted = false;
                        context.branches.Add(md);
                        context.Entry(md).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        md = context.branches.FirstOrDefault(m => m.id == model.ID);
                        md.name = model.Name;
                        md.address = model.Address;
                        md.phone = model.Phone;
                        md.hotline = model.Hotline;
                        md.open_day = model.OpenDate;
                        md.notes = model.Notes;
                        md.location_id = model.LocationID;
                        md.update_by = model.UpdatedBy;
                        md.update_date = DateTime.Now;
                        context.branches.Attach(md);
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
                    var md = context.branches.First(m => m.id == id);
                    md.deleted = true;
                    md.delete_by = userID;
                    md.delete_date = DateTime.Now;
                    context.branches.Attach(md);
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
