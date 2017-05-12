using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// contract type model
    /// </summary>
    public class ContractTypeModel
    {

        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID không được rỗng")]
        public Guid Code { get; set; }

        [Required(ErrorMessage = "Chi nhánh không được rỗng")]
        [StringLength(50, ErrorMessage = "Tên loại hợp đồng không vượt quá 50 ký tự")]
        public string Name { get; set; }
    }
}
