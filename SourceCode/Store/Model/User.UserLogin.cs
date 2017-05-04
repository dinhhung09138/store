using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.User
{
    /// <summary>
    /// User Login Model
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Token id
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Device ID, login from mobile
        /// </summary>
        public string DeviceID { get; set; }
    }
}
