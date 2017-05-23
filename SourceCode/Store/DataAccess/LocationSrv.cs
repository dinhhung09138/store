using Common.Message;
using Common.Status;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess
{
    public class LocationSrv
    {
        public List<LocationModel> FindByName(string name)
        {
            List<LocationModel> _return = new List<LocationModel>();

            using (var context = new StoreEntities())
            {
                var list = context.locations.Where(m => m.name.ToLower().Contains(name)).ToList();
                foreach (var item in list)
                {
                    _return.Add(new LocationModel() { ID = item.id, Name = item.name });
                }
            }
            return _return;
        }
    }
}
