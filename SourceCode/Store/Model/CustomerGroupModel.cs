using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Group of customer
    /// </summary>
    public class CustomerGroupModel : Base
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID nhóm không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Tên nhóm không được rỗng")]
        [StringLength(50, ErrorMessage = "Tên nhóm không vượt quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, ErrorMessage = "Ghi chú không vượt quá 255 ký tự")]
        public string Notes { get; set; }
    }
}
