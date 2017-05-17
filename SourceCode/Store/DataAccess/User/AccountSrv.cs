using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.User
{
    public class AccountSrv
    {

        public bool Login(string userName, string passWord)
        {
            using (var context = new StoreEntities())
            {
                var it = context.users.Where(a => a.user_name == userName && a.password == passWord).FirstOrDefault();
                if(it.user_name != null && it.user_name.Length > 0)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
