using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Supplier class
    /// </summary>
    public class SupplierModel : Base
    {
        /// <summary>
        /// Supplier id
        /// </summary>
        [Required(ErrorMessage = "ID nhà cung cấp không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Code of supplier
        /// </summary>
        [Required(ErrorMessage = "Mã không được rỗng")]
        [StringLength(20, ErrorMessage = "Mã không vượt quá 20 ký tự")]
        public string Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Tên không được rỗng")]
        [StringLength(50, ErrorMessage = "Tên không vượt quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Avatar
        /// </summary>
        [StringLength(100, ErrorMessage = "Đường dẫn ảnh không vượt quá 100 ký tự")]
        public string Avatar { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 50 ký tự")]
        public string Phone { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [StringLength(50, ErrorMessage = "Địa chỉ không vượt quá 50 ký tự")]
        public string Address { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(50, ErrorMessage = "Email không vượt quá 50 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        /// <summary>
        /// id of group which belong to
        /// </summary>
        public Guid GroupID { get; set; }

        /// <summary>
        /// Name of group which belong to
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Tax code
        /// </summary>
        [StringLength(25, ErrorMessage = "Mã số thuế không vượt quá 25 ký tự")]
        public string TaxCode { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        [StringLength(50, ErrorMessage = "Tên công ty không vượt quá 50 ký tự")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(266, ErrorMessage = "Ghi chú không vượt quá 255 ký tự")]
        public string Notes { get; set; }
    }
}
