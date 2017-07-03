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
    public class GoogleAnalyst
    {
        /// <summary>
        /// Save account item
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, object> SaveAccount(GAAccount model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    ga_account md = new ga_account();
                    md = context.ga_account.FirstOrDefault(m => m.view_id == model.ViewID);
                    if(md != null)
                    {
                        md.view_id = model.ViewID;
                        md.view_name = model.ViewName;
                        md.property_id = model.PropertyID;
                        md.property_name = model.PropertyName;
                        md.acc_id = model.AccountID;
                        md.acc_name = model.AccountName;
                        md.create_by = model.CreateBy;
                        md.create_date = DateTime.Now;
                        context.Entry(md).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        md = new ga_account();
                        md.view_id = model.ViewID;
                        md.view_name = model.ViewName;
                        md.property_id = model.PropertyID;
                        md.property_name = model.PropertyName;
                        md.acc_id = model.AccountID;
                        md.acc_name = model.AccountName;
                        md.update_by = model.UpdatedBy;
                        md.update_date = DateTime.Now;
                        context.Entry(md).State = System.Data.Entity.EntityState.Added;
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
        /// Save City list
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, object> SaveDeviceCategory(List<GADeviceCategory> model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {

                    foreach (var item in model)
                    {
                        ga_user_city md = new ga_user_city();
                        md = context.ga_user_city.FirstOrDefault(m => m.table_id == item.TableID && m.day == item.Day && m.month == item.Month && m.year == item.Year);
                        if(md == null)
                        {
                            md = new ga_user_city();
                            md.id = Guid.NewGuid();
                            md.table_id = item.TableID;

                        }
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

    }
}
