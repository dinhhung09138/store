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
    /// Customer service
    /// </summary>
    public class CustomerSrv
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
                JqueryDataTableResponse<CustomerModel> itemResponse = new JqueryDataTableResponse<CustomerModel>();
                //List of data
                List<CustomerModel> _list = new List<CustomerModel>();
                using (var context = new StoreEntities())
                {
                    var l = (from a in context.customers
                             where !a.deleted
                             orderby a.name
                             select new
                             {
                                 a.id,
                                 a.code,
                                 a.gender,
                                 genderString = a.gender == true ? "Nam" : "Nữ",
                                 a.name,
                                 a.address,
                                 a.phone
                             }).ToList();

                    itemResponse.draw = request.draw;
                    itemResponse.recordsTotal = l.Count;
                    //Search
                    if (request.search != null && !string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        l = l.Where(m => m.code.ToLower().Contains(searchValue) ||
                                    m.name.ToLower().Contains(searchValue) ||
                                    m.genderString.ToLower().Contains(searchValue) ||
                                    m.address.ToLower().Contains(searchValue) ||
                                    m.phone.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in l)
                    {
                        _list.Add(new CustomerModel()
                        {
                            ID = item.id,
                            Code = item.code,
                            Name = item.name,
                            GenderString = item.genderString,
                            Address = item.address,
                            Phone = item.phone
                        });
                    }
                    itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<CustomerModel> _sortList = null;
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
                                case "GenderString":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.GenderString) : _sortList.Sort(col.Dir, m => m.GenderString);
                                    break;
                                case "Phone":
                                    _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Phone) : _sortList.Sort(col.Dir, m => m.Phone);
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
        public CustomerModel Item(Guid id)
        {
            CustomerModel _item = new CustomerModel() { ID = Guid.NewGuid() };
            try
            {
                
                using (var context = new StoreEntities())
                {
                    var item = (from m in context.customers
                                join g in context.customer_group on m.group_id equals g.id
                                join l in context.locations on m.location_id equals l.id
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
                                    m.location_id,
                                    LocationName = l.name,
                                    m.maps,
                                    m.taxcode,
                                    m.is_company,
                                    m.gender,
                                    m.group_id,
                                    GroupName = g.name,
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
                    _item.LocationID = item.location_id;
                    _item.LocationName = item.LocationName;
                    _item.Maps = item.maps;
                    _item.TaxCode = item.taxcode;
                    _item.IsCompany = item.is_company;
                    _item.Gender = item.gender;
                    _item.GroupID = item.group_id;
                    _item.GroupName = item.GroupName;
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
        public Dictionary<string, object> Save(CustomerModel model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    customer md = new customer();
                    if (model.Insert)
                    {
                        md.id = Guid.NewGuid();
                        md.name = model.Name;
                        md.code = model.Code;
                        md.birthdate = model.Birthdate;
                        md.phone = model.Phone;
                        md.address = model.Address;
                        md.avatar = model.Avatar;
                        md.location_id = model.LocationID;
                        md.maps = model.Maps;
                        md.taxcode = model.TaxCode;
                        md.is_company = model.IsCompany;
                        md.gender = model.Gender;
                        md.notes = model.Notes;
                        md.create_by = model.CreateBy;
                        md.create_date = DateTime.Now;
                        md.deleted = false;
                        context.customers.Add(md);
                        context.Entry(md).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        md = context.customers.FirstOrDefault(m => m.id == model.ID);
                        md.name = model.Name;
                        md.code = model.Code;
                        md.birthdate = model.Birthdate;
                        md.phone = model.Phone;
                        md.address = model.Address;
                        md.avatar = model.Avatar;
                        md.location_id = model.LocationID;
                        md.maps = model.Maps;
                        md.taxcode = model.TaxCode;
                        md.is_company = model.IsCompany;
                        md.gender = model.Gender;
                        md.notes = model.Notes;
                        md.update_by = model.UpdatedBy;
                        md.update_date = DateTime.Now;
                        context.customers.Attach(md);
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
                    var md = context.customers.First(m => m.id == id);
                    md.deleted = true;
                    md.delete_by = userID;
                    md.delete_date = DateTime.Now;
                    context.customers.Attach(md);
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
