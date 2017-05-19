using System;
using System.ComponentModel.DataAnnotations;

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
        [StringLength(20, ErrorMessage = "Mã không vượt quá 20 ký tự")]
        public string Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Tên không được rỗng")]
        [StringLength(50, ErrorMessage = "Tên không vượt quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Birthdate
        /// </summary>
        public DateTime? Birthdate { get; set; }

        /// <summary>
        /// Gender true: Male, false: Female
        /// </summary>
        [Required]
        public bool Gender { get; set; }
        
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
        /// true: Company, false: individual
        /// </summary>
        public bool IsCompany { get; set; }

        /// <summary>
        /// Tax code
        /// </summary>
        [StringLength(20, ErrorMessage = "Mã số thuế không vượt quá 20 ký tự")]
        public string TaxCode { get; set; }

        /// <summary>
        /// group's id
        /// </summary>
        public Guid? GroupID { get; set; }

        /// <summary>
        /// group's name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, ErrorMessage = "Ghi chú không vượt quá 255 ký tự")]
        public string Notes { get; set; }

    }
}
