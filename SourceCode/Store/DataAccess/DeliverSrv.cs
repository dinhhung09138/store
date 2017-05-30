﻿using Common;
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
    /// Deliver service
    /// </summary>
    public class DeliverSrv
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
                JqueryDataTableResponse<DeliverModel> itemResponse = new JqueryDataTableResponse<DeliverModel>();
                //List of data
                List<DeliverModel> _list = new List<DeliverModel>();
                using (var context = new StoreEntities())
                {
                    var l = (from a in context.delivers
                             join g in context.deliver_group on a.group_id equals g.id into group_deliver
                             from g1 in group_deliver.DefaultIfEmpty()
                             join loc in context.locations on a.location_id equals loc.id into lc_cus
                             from l1 in lc_cus.DefaultIfEmpty()
                             where !a.deleted
                             orderby a.name
                             select new
                             {
                                 a.id,
                                 a.name,
                                 a.address,
                                 a.phone,
                                 group_name = g1.name,
                                 location_name = l1.name
                             }).ToList();
                    itemResponse.draw = request.draw;
                    itemResponse.recordsTotal = l.Count;
                    //Search
                    if (!string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        l = l.Where(m => m.name.ToLower().Contains(searchValue) ||
                                    m.address.ToLower().Contains(searchValue) ||
                                    m.phone.ToLower().Contains(searchValue) ||
                                    m.group_name.ToLower().Contains(searchValue) ||
                                    m.location_name.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in l)
                    {
                        _list.Add(new DeliverModel()
                        {
                            ID = item.id,
                            Name = item.name,
                            Address = item.address,
                            Phone = item.phone,
                            GroupName = item.group_name,
                            LocationName =item.location_name
                        });
                    }
                    itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<DeliverModel> _sortList = null;
                    foreach (var col in request.order)
                    {
                        switch (col.ColumnName)
                        {
                            case "Name":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
                                break;
                            case "Address":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Address) : _sortList.Sort(col.Dir, m => m.Address);
                                break;
                            case "Phone":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Phone) : _sortList.Sort(col.Dir, m => m.Phone);
                                break;
                            case "GroupName":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.GroupName) : _sortList.Sort(col.Dir, m => m.GroupName);
                                break;
                            case "LocationName":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.LocationName) : _sortList.Sort(col.Dir, m => m.LocationName);
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
        public DeliverModel Item(Guid id)
        {
            DeliverModel _item = new DeliverModel() { ID = Guid.NewGuid() };
            try
            {
                using (var context = new StoreEntities())
                {
                    var item = (from m in context.delivers
                                join g in context.deliver_group on m.group_id equals g.id into group_deliver
                                from g1 in group_deliver.DefaultIfEmpty()
                                join l in context.locations on m.location_id equals l.id into location_deliver
                                from l1 in location_deliver.DefaultIfEmpty()
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
                                    LocationName = l1.name,
                                    m.taxcode,
                                    m.is_company,
                                    m.gender,
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
                    _item.LocationID = item.location_id;
                    _item.LocationName = item.LocationName;
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
        /// Return dynamic item code
        /// </summary>
        /// <returns></returns>
        public static string GetCode()
        {
            try
            {
                using (var context = new StoreEntities())
                {
                    int count = context.delivers.Count();
                    return Utils.DELIVER_CODE + count.ReturnTo9Digit();
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
        public Dictionary<string, object> Save(DeliverModel model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    deliver md = new deliver();
                    if (model.Insert)
                    {
                        md.id = Guid.NewGuid();
                        md.name = model.Name;
                        md.code = model.Code;
                        md.birthdate = model.Birthdate;
                        md.phone = model.Phone;
                        md.address = model.Address;
                        md.avatar = model.Avatar;
                        md.group_id = model.GroupID;
                        md.location_id = model.LocationID;
                        md.taxcode = model.TaxCode;
                        md.is_company = model.IsCompany;
                        md.gender = model.Gender;
                        md.notes = model.Notes;
                        md.create_by = model.CreateBy;
                        md.create_date = DateTime.Now;
                        md.deleted = false;
                        context.delivers.Add(md);
                        context.Entry(md).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        md = context.delivers.FirstOrDefault(m => m.id == model.ID);
                        md.name = model.Name;
                        md.code = model.Code;
                        md.birthdate = model.Birthdate;
                        md.phone = model.Phone;
                        md.address = model.Address;
                        md.avatar = model.Avatar;
                        md.group_id = model.GroupID;
                        md.location_id = model.LocationID;
                        md.taxcode = model.TaxCode;
                        md.is_company = model.IsCompany;
                        md.gender = model.Gender;
                        md.notes = model.Notes;
                        md.update_by = model.UpdatedBy;
                        md.update_date = DateTime.Now;
                        context.delivers.Attach(md);
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
                    var md = context.delivers.First(m => m.id == id);
                    md.deleted = true;
                    md.delete_by = userID;
                    md.delete_date = DateTime.Now;
                    context.delivers.Attach(md);
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
