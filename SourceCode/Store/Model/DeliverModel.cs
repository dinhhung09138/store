using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Delivers partner
    /// </summary>
    public class DeliverModel : Base
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        [Required(ErrorMessage = "Mã không được rỗng")]
        [StringLength(20, ErrorMessage = "Mã KH không vượt quá 20 ký tự")]
        public string Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Tên không được rỗng")]
        [StringLength(50, ErrorMessage = "Tên không vượt quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [Required(ErrorMessage = "Số điện thoại không được rỗng")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(50, ErrorMessage = "Email không vượt quá 50 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [StringLength(50, ErrorMessage = "Địa chỉ không vượt quá 50 ký tự")]
        public string Address { get; set; }

    }
}
