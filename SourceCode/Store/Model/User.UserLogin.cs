using System;
using System.ComponentModel.DataAnnotations;

namespace Model.User
{
    /// <summary>
    /// User Login Model
    /// </summary>
    public class UserLoginModel
    {
        /// <summary>
        /// User ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        [Required(ErrorMessage = "Tên đăng nhập không được rỗng")]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Mật khẩu không được rỗng")]
        public string Password { get; set; }

        /// <summary>
        /// Token id
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Device ID, login from mobile
        /// </summary>
        public string DeviceID { get; set; }

        /// <summary>
        /// Remember me
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
