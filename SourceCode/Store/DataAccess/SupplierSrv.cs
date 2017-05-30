using Common;
using Common.JqueryDataTable;
using Common.Message;
using Common.Status;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess
{
    /// <summary>
    /// Supllier service
    /// </summary>
    public class SupplierSrv
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
                JqueryDataTableResponse<SupplierModel> itemResponse = new JqueryDataTableResponse<SupplierModel>();
                //List of data
                List<SupplierModel> _list = new List<SupplierModel>();
                using (var context = new StoreEntities())
                {
                    var l = (from a in context.suppliers
                             where !a.deleted
                             orderby a.name
                             select new
                             {
                                 a.id,
                                 a.name,
                                 a.address,
                                 a.phone,
                                 a.company_name
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
                                    m.company_name.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in l)
                    {
                        _list.Add(new SupplierModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            Address = item.address,
                            Phone = item.phone,
                            CompanyName = item.company_name
                        });
                    }
                    itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<SupplierModel> _sortList = null;
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
                                case "Address":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Address) : _sortList.Sort(col.Dir, m => m.Address);
                                    break;
                                case "CompanyName":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.CompanyName) : _sortList.Sort(col.Dir, m => m.CompanyName);
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
        /// Get item
        /// </summary>
        /// <param name="id">id of item</param>
        /// <returns></returns>
        public SupplierModel Item(Guid id)
        {
            SupplierModel _item = new SupplierModel() { ID = Guid.NewGuid() };
            try
            {

                using (var context = new StoreEntities())
                {
                    var item = (from m in context.suppliers
                                join g in context.supplier_group on m.group_id equals g.id into grour_spl
                                from g1 in grour_spl.DefaultIfEmpty()
                                where m.id == id
                                select new
                                {
                                    m.id,
                                    m.code,
                                    m.name,
                                    m.birthdate,
                                    m.phone,
                                    m.email,
                                    m.address,
                                    m.avatar,
                                    m.taxcode,
                                    m.company_name,
                                    m.group_id,
                                    GroupName = g1.name,
                                    m.notes
                                }).First();
                    _item.ID = item.id;
                    _item.Code = item.code;
                    _item.Name = item.name;
                    _item.Birthdate = item.birthdate;
                    _item.Phone = item.phone;
                    _item.Email = item.email;
                    _item.Address = item.address;
                    _item.Avatar = item.avatar;
                    _item.TaxCode = item.taxcode;
                    _item.CompanyName = item.company_name;
                    _item.GroupName = item.GroupName;
                    _item.Notes = item.notes;
                    _item.ImageFileName = "";
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
        /// Return dynamic item code
        /// </summary>
        /// <returns></returns>
        public static string GetCode()
        {
            try
            {
                using (var context = new StoreEntities())
                {
                    int count = context.suppliers.Count();
                    return Utils.SUPPLIER_CODE + count.ReturnTo9Digit();
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Save item
        /// </summary>
        /// <param name="model">Motel</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, object> Save(SupplierModel model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    supplier md = new supplier();
                    if (model.Insert)
                    {
                        md.id = Guid.NewGuid();
                        md.name = model.Name;
                        md.code = model.Code;
                        md.birthdate = model.Birthdate;
                        md.phone = model.Phone;
                        md.email = model.Email;
                        md.address = model.Address;
                        md.avatar = model.Avatar;
                        md.group_id = model.GroupID;
                        md.company_name = model.CompanyName;
                        md.taxcode = model.TaxCode;
                        md.notes = model.Notes;
                        md.create_by = model.CreateBy;
                        md.create_date = DateTime.Now;
                        md.deleted = false;
                        context.suppliers.Add(md);
                        context.Entry(md).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        md = context.suppliers.FirstOrDefault(m => m.id == model.ID);
                        md.name = model.Name;
                        md.code = model.Code;
                        md.birthdate = model.Birthdate;
                        md.phone = model.Phone;
                        md.address = model.Address;
                        md.email = model.Email;
                        md.avatar = model.Avatar;
                        md.group_id = model.GroupID;
                        md.taxcode = model.TaxCode;
                        md.company_name = model.CompanyName;
                        md.notes = model.Notes;
                        md.update_by = model.UpdatedBy;
                        md.update_date = DateTime.Now;
                        context.suppliers.Attach(md);
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
                    var md = context.suppliers.First(m => m.id == id);
                    md.deleted = true;
                    md.delete_by = userID;
                    md.delete_date = DateTime.Now;
                    context.suppliers.Attach(md);
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
