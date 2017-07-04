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
        /// Save User
        /// ga:users,ga:newUsers,ga:sessions,ga:bounces,ga:bounceRate,ga:entrances,ga:entranceRate
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, object> SaveUser01(List<GAUserType> model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    foreach (var item in model)
                    {
                        ga_user_type md = new ga_user_type();
                        md = context.ga_user_type.FirstOrDefault(m => m.table_id == item.TableID && m.day == item.Day && m.month == item.Month && m.year == item.Year);
                        if(md == null) //Insert
                        {
                            md = new ga_user_type();
                            md.id = Guid.NewGuid();
                            md.table_id = item.TableID;
                            md.day = item.Day;
                            md.month = item.Month;
                            md.year = item.Year;
                            md.user = item.Users;
                            md.new_user = item.NewUsers;
                            md.return_user = item.Users - item.NewUsers;
                            md.sessions = item.Sessions;
                            md.bounces = item.Bounces;
                            md.bounce_rate = item.Bouncerate;
                            md.entrances = item.Entrances;
                            //entranstra TODO
                            context.ga_user_type.Add(md);
                            context.Entry(md).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            md.user = item.Users;
                            md.new_user = item.NewUsers;
                            md.return_user = item.Users - item.NewUsers;
                            md.sessions = item.Sessions;
                            md.bounces = item.Bounces;
                            md.bounce_rate = item.Bouncerate;
                            md.entrances = item.Entrances;
                            //entranstra TODO
                            context.ga_user_type.Attach(md);
                            context.Entry(md).State = System.Data.Entity.EntityState.Modified;
                        }
                        context.SaveChanges();
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
        /// Save User
        /// ga:pageviews,ga:exits,ga:exitrate,ga:pageviewspersession,ga:timeonpage,ga:avgTimeOnPage
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, object> SaveUser02(List<GAUserType> model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    foreach (var item in model)
                    {
                        ga_user_type md = new ga_user_type();
                        md = context.ga_user_type.FirstOrDefault(m => m.table_id == item.TableID && m.day == item.Day && m.month == item.Month && m.year == item.Year);
                        if (md == null) //Insert
                        {
                            md = new ga_user_type();
                            md.id = Guid.NewGuid();
                            md.table_id = item.TableID;
                            md.day = item.Day;
                            md.month = item.Month;
                            md.year = item.Year;
                            md.pageviews = item.PageViews;
                            //TODO md.exits = item.Exits;
                            //TODO md.exit_rate= item.Users - item.ExitsRate;
                            //TODO md.pageview_per_session = item.UniquePageViews;
                            //TODO md.timeonpage = item.TimeOnPage;
                            //TODO md.bounce_rate = item.AvgTimeOnPage;
                            
                            //entranstra TODO
                            context.ga_user_type.Add(md);
                            context.Entry(md).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            md.user = item.Users;
                            md.new_user = item.NewUsers;
                            md.return_user = item.Users - item.NewUsers;
                            md.sessions = item.Sessions;
                            md.bounces = item.Bounces;
                            md.bounce_rate = item.Bouncerate;
                            md.entrances = item.Entrances;
                            //entranstra TODO
                            context.ga_user_type.Attach(md);
                            context.Entry(md).State = System.Data.Entity.EntityState.Modified;
                        }
                        context.SaveChanges();
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
        /// Save City
        /// ga:users,ga:newUsers,ga:sessions,ga:bounces,ga:bounceRate,ga:entrances,ga:entranceRate
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, object> SaveCity01(List<GAUserCity> model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    foreach (var item in model)
                    {
                        ga_user_city md = new ga_user_city();
                        md = context.ga_user_city.FirstOrDefault(m => m.table_id == item.TableID && m.city == item.City && m.day == item.Day && m.month == item.Month && m.year == item.Year);
                        if (md == null) //Insert
                        {
                            md = new ga_user_city();
                            md.id = Guid.NewGuid();
                            md.table_id = item.TableID;
                            md.city = item.City;
                            md.day = item.Day;
                            md.month = item.Month;
                            md.year = item.Year;
                            md.user = item.Users;
                            md.new_user = item.NewUsers;
                            md.return_user = item.Users - item.NewUsers;
                            md.sessions = item.Sessions;
                            md.bounces = item.Bounces;
                            md.bounce_rate = item.Bouncerate;
                            md.entrances = item.Entrances;
                            //entranstra TODO
                            context.ga_user_city.Add(md);
                            context.Entry(md).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            md.user = item.Users;
                            md.new_user = item.NewUsers;
                            md.return_user = item.Users - item.NewUsers;
                            md.sessions = item.Sessions;
                            md.bounces = item.Bounces;
                            md.bounce_rate = item.Bouncerate;
                            md.entrances = item.Entrances;
                            //entranstra TODO
                            context.ga_user_city.Attach(md);
                            context.Entry(md).State = System.Data.Entity.EntityState.Modified;
                        }
                        context.SaveChanges();
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
        /// Save City
        /// ga:pageviews,ga:exits,ga:exitrate,ga:pageviewspersession,ga:timeonpage,ga:avgTimeOnPage
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, object> SaveCity02(List<GAUserCity> model)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                using (var context = new StoreEntities())
                {
                    foreach (var item in model)
                    {
                        ga_user_city md = new ga_user_city();
                        md = context.ga_user_city.FirstOrDefault(m => m.table_id == item.TableID && m.city == item.City && m.day == item.Day && m.month == item.Month && m.year == item.Year);
                        if (md == null) //Insert
                        {
                            md = new ga_user_city();
                            md.id = Guid.NewGuid();
                            md.table_id = item.TableID;
                            md.city = item.City;
                            md.day = item.Day;
                            md.month = item.Month;
                            md.year = item.Year;
                            md.pageviews = item.PageViews;
                            //TODO md.exits = item.Exits;
                            //TODO md.exit_rate= item.Users - item.ExitsRate;
                            //TODO md.pageview_per_session = item.UniquePageViews;
                            //TODO md.timeonpage = item.TimeOnPage;
                            //TODO md.bounce_rate = item.AvgTimeOnPage;

                            //entranstra TODO
                            context.ga_user_city.Add(md);
                            context.Entry(md).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            md.user = item.Users;
                            md.new_user = item.NewUsers;
                            md.return_user = item.Users - item.NewUsers;
                            md.sessions = item.Sessions;
                            md.bounces = item.Bounces;
                            md.bounce_rate = item.Bouncerate;
                            md.entrances = item.Entrances;
                            //entranstra TODO
                            context.ga_user_city.Attach(md);
                            context.Entry(md).State = System.Data.Entity.EntityState.Modified;
                        }
                        context.SaveChanges();
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

    }
}
