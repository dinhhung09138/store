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
    /// Delivery status service
    /// </summary>
    public class DeliverStatusSrv
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
                JqueryDataTableResponse<DeliveryStatusModel> itemResponse = new JqueryDataTableResponse<DeliveryStatusModel>();
                //List of data
                List<DeliveryStatusModel> _list = new List<DeliveryStatusModel>();
                using (var context = new StoreEntities())
                {
                    var l = (from a in context.delivery_status orderby a.name select new { a.id, a.name }).ToList();
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
                        _list.Add(new DeliveryStatusModel()
                        {
                            ID = item.id,
                            Name = item.name,
                        });
                    }
                    itemResponse.recordsFiltered = _list.Count;
                    IOrderedEnumerable<DeliveryStatusModel> _sortList = null;
                    foreach (var col in request.order)
                    {
                        switch (col.ColumnName)
                        {
                            case "Name":
                                _sortList = _sortList == null ? _list.Sort(col.Dir, m => m.Name) : _sortList.Sort(col.Dir, m => m.Name);
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
        public Dictionary<string, object> Item(byte id)
        {
            Dictionary<string, object> _return = new Dictionary<string, object>();
            try
            {
                DeliveryStatusModel _item = new DeliveryStatusModel();
                using (var context = new StoreEntities())
                {
                    var item = context.delivery_status.First(m => m.id == id);
                    _item.ID = id;
                    _item.Name = item.name;
                }
                _return.Add("status", DatabaseExecute.Success);
                _return.Add("data", _item);
            }
            catch (Exception ex)
            {
                _return.Add("status", DatabaseExecute.Error);
                _return.Add("systemMessage", ex.Message);
                _return.Add("message", DatabaseMessage.ITEM_ERROR);
            }

            return _return;
        }
        
    }
}
