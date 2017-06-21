using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Group of supplier
    /// </summary>
    public class SupplierGroupModel : Base
    {
        /// <summary>
        /// ID of group
        /// </summary>
        [Required(ErrorMessage = "ID Nhóm không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Name of group
        /// </summary>
        [Required(ErrorMessage = "Nhóm nhà cung cấp không được rỗng")]
        [StringLength(50, ErrorMessage = "Tên nhóm không vượt quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, ErrorMessage = "Ghi chú không vượt quá 255 ký tự")]
        public string Notes { get; set; }
    }
}
