using System.Linq;
using Model.User;

namespace DataAccess.User
{
    /// <summary>
    /// Account login
    /// </summary>
    public class AccountSrv
    {

        public UserLoginModel Login(string userName, string passWord)
        {
            using (var context = new StoreEntities())
            {
                var it = context.users.Where(a => a.user_name == userName && a.password == passWord).FirstOrDefault();
                if (it == null || it.user_name.Length == 0)
                {
                    return null;
                }
                UserLoginModel item = new UserLoginModel();
                item.ID = it.id;
                item.UserName = it.user_name;
                item.Token = "";
                item.DeviceID = "";
                return item;
            }
        }

    }
}
