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
    public class EmployeeSrv
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
                JqueryDataTableResponse<EmployeeModel> itemResponse = new JqueryDataTableResponse<EmployeeModel>();
                //List of data
                List<EmployeeModel> _list = new List<EmployeeModel>();
                using (var context = new StoreEntities())
                {
                    var l = (from a in context.employees
                             join c in context.contract_type on a.contract_type_code equals c.code into ContactType
                             from ct in ContactType.DefaultIfEmpty()
                             where !a.deleted
                             orderby a.name
                             select new
                             {
                                 a.id,
                                 a.name,
                                 a.code,
                                 a.address,
                                 a.phone,
                                 ContractTypeName = ct.name
                             }).ToList();
                    itemResponse.draw = request.draw;
                    itemResponse.recordsTotal = l.Count;
                    //Search
                    if (!string.IsNullOrWhiteSpace(request.search.Value))
                    {
                        string searchValue = request.search.Value.ToLower();
                        l = l.Where(m => m.name.ToLower().Contains(searchValue) ||
                                    m.code.ToLower().Contains(searchValue) ||
                                    m.address.ToLower().Contains(searchValue) ||
                                    m.phone.ToLower().Contains(searchValue)).ToList();
                    }
                    //Add to list
                    foreach (var item in l)
                    {
                        _list.Add(new EmployeeModel()
                        {
                            ID = item.id,
                            Code = item.code,
                            Name = item.name,
                            Address = item.address,
                            Phone = item.phone,
                            ContractTypeName = item.ContractTypeName
                        });
                    }
                    itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<EmployeeModel> _sortList = null;
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
                            case "Address":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Address) : _sortList.Sort(col.Dir, m => m.Address);
                                break;
                            case "Phone":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Phone) : _sortList.Sort(col.Dir, m => m.Phone);
                                break;
                            case "ContractTypeName":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.ContractTypeName) : _sortList.Sort(col.Dir, m => m.ContractTypeName);
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
                    int count = context.employees.Count();
                    return Utils.EMPLOYEE_CODE + count.ReturnTo9Digit();
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
        public EmployeeModel Item(Guid id)
        {
            EmployeeModel _item = new EmployeeModel();
            try
            {
                using (var context = new StoreEntities())
                {
                    var item = (from m in context.employees
                                join g in context.contract_type on m.contract_type_code equals g.code into grour
                                from g1 in grour.DefaultIfEmpty()
                                where m.id == id
                                select new
                                {
                                    m.id,
                                    m.code,
                                    m.name,
                                    m.birthdate,
                                    m.gender,
                                    m.phone,
                                    m.email,
                                    m.address,
                                    m.avatar,
                                    m.id_card,
                                    m.start_working_date,
                                    m.end_working_date,
                                    m.contract_type_code,
                                    GroupName = g1.name,
                                }).First();
                    _item.ID = item.id;
                    _item.Code = item.code;
                    _item.Name = item.name;
                    _item.Birthdate = item.birthdate;
                    _item.Phone = item.phone;
                    _item.Email = item.email;
                    _item.Address = item.address;
                    _item.Avatar = item.avatar;
                    _item.IDCard = item.id_card;
                    _item.Gender = item.gender;
                    _item.StartWorkingDate = item.start_working_date;
                    _item.EndWorkingDate = item.end_working_date;
                    _item.ContractTypeCode = item.contract_type_code;
                    _item.ContractTypeName = item.GroupName;
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
        public Dictionary<string, object> Save(EmployeeModel model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    employee md = new employee();
                    if (model.Insert)
                    {
                        md.id = Guid.NewGuid();
                        md.name = model.Name;
                        md.code = model.Code;
                        md.birthdate = model.Birthdate;
                        md.phone = model.Phone;
                        md.address = model.Address;
                        md.email = model.Email;
                        md.avatar = model.Avatar;
                        //md.contract_type_code = model.ContractTypeCode;
                        md.id_card = model.IDCard;
                        md.gender = model.Gender;
                        md.start_working_date = model.StartWorkingDate;
                        md.end_working_date = model.EndWorkingDate;
                        md.create_by = model.CreateBy;
                        md.create_date = DateTime.Now;
                        md.deleted = false;
                        context.employees.Add(md);
                        context.Entry(md).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        md = context.employees.FirstOrDefault(m => m.id == model.ID);
                        md.name = model.Name;
                        md.code = model.Code;
                        md.birthdate = model.Birthdate;
                        md.phone = model.Phone;
                        md.address = model.Address;
                        md.email = model.Email;
                        md.avatar = model.Avatar;
                        //md.contract_type_code = model.ContractTypeCode;
                        md.id_card = model.IDCard;
                        md.gender = model.Gender;
                        md.start_working_date = model.StartWorkingDate;
                        md.end_working_date = model.EndWorkingDate;
                        md.update_by = model.UpdatedBy;
                        md.update_date = DateTime.Now;
                        context.employees.Attach(md);
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
                    var md = context.employees.First(m => m.id == id);
                    md.deleted = true;
                    md.delete_by = userID;
                    md.delete_date = DateTime.Now;
                    context.employees.Attach(md);
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
        /// Get list employeeModel to display on dropdown controll (or for something else) in other form
        /// </summary>
        /// <returns></returns>
        public List<EmployeeModel> GetListForDisplay()
        {
            List<EmployeeModel> _return = new List<EmployeeModel>();
            using (var context = new StoreEntities())
            {
                var list = (from a in context.employees where !a.deleted orderby a.name select new { a.id, a.name }).ToList();
                foreach (var item in list)
                {
                    _return.Add(new EmployeeModel() { ID = item.id, Name = item.name });
                }
            }
            return _return;
        }
    }
}
