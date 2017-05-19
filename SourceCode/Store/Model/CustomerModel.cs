using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Customer
    /// </summary>
    public class CustomerModel : Base
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID KH không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        [Required(ErrorMessage = "Mã KH không được rỗng")]
        [StringLength(20, ErrorMessage = "Mã KH không vượt quá 20 ký tự")]
        public string Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Tên KH không được rỗng")]
        [StringLength(50, ErrorMessage = "Tên KH không vượt quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Birthday
        /// </summary>
        public DateTime? Birthdate { get; set; }

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

        /// <summary>
        /// Avatar
        /// </summary>
        [StringLength(100, ErrorMessage = "Đường dẫn ảnh không vượt quá 100 ký tự")]
        public string Avatar { get; set; }

        /// <summary>
        /// Location's id
        /// </summary>
        public Guid? LocationID { get; set; }


        /// <summary>
        /// Location's name
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Map position (longtitude, latitude)
        /// </summary>
        [StringLength(500, ErrorMessage = "Đường đẫn bản đồ không vượt quá 500 ký tự")]
        public string Maps { get; set; }

        /// <summary>
        /// Tax code
        /// </summary>
        [StringLength(20, ErrorMessage = "Mã số thuế không vượt quá 20 ký tự")]
        public string TaxCode { get; set; }

        /// <summary>
        /// Gender true: Male, false: Female
        /// </summary>
        [Required]
        public bool Gender { get; set; }

        /// <summary>
        /// true: Company, false: personal
        /// </summary>
        [Required]
        public bool IsCompany { get; set; }

        /// <summary>
        /// Group of customer's id
        /// </summary>
        public Guid? GroupID { get; set; }

        /// <summary>
        /// groups of customer's name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, ErrorMessage = "Ghi chú không vượt quá 255 ký tự")]
        public string Notes { get; set; }
    }
}
