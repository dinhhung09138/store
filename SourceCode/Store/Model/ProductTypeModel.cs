using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Type of product
    /// </summary>
    public class ProductTypeModel : Base
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID loại sản phẩm không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Tên loại không vượt quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, ErrorMessage = "Ghi chú không vượt quá 255 ký tự")]
        public string Notes { get; set; }
    }
}
