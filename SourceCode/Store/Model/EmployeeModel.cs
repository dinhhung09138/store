using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Employee model
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID không được rỗng")]
        public Guid ID { get; set; }

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
        /// Gender: true: male, false: female
        /// </summary>
        public bool Gender { get; set; } = true;

        /// <summary>
        /// Birthdate
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Employee's image
        /// </summary>
        [StringLength(100, ErrorMessage = "Đường dẫn ảnh không vượt quá 100 ký tự")]
        public string Image { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [StringLength(50, ErrorMessage = "Địa chỉ không vượt quá 50 ký tự")]
        public string Address { get; set; }

        /// <summary>
        /// ID card
        /// </summary>
        [StringLength(12, ErrorMessage = "CMND không vượt quá 12 ký tự")]
        public string IDCard { get; set; }

        /// <summary>
        /// Location's id
        /// </summary>
        public Guid LocationID { get; set; }

        /// <summary>
        /// Location's name
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Ngày bắt đầu làm việc
        /// </summary>
        [Required(ErrorMessage = "Ngày bắt đầu làm không được rỗng")]
        public DateTime StartWorkingDate { get; set; }

        /// <summary>
        /// Ngày kết thúc làm việc
        /// </summary>
        public DateTime EndWorkingDate { get; set; }

        /// <summary>
        /// Contract type's code
        /// </summary>
        [Required(ErrorMessage = "Loại hợp đồng không được rỗng")]
        public Guid ContractTypeCode { get; set; }

        /// <summary>
        /// Contract type's name
        /// </summary>
        public string ContractTypeName { get; set; }


    }
}
