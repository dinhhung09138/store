using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Branch model
    /// </summary>
    public class BranchModel : Base
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID chi nhánh không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Name of branch
        /// </summary>
        [Required(ErrorMessage = "Tên chi nhánh không được rỗng")]
        [StringLength(150, ErrorMessage = "Tên chi nhánh không vượt quá 150 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [StringLength(200, ErrorMessage = "Địa chỉ không vượt quá 200 ký tự")]
        public string Address { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [Required(ErrorMessage = "Số điện thoại không được rỗng")]
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        public string Phone { get; set; }

        /// <summary>
        /// Hotline
        /// </summary>
        [StringLength(15, ErrorMessage = "Số điện thoại không vượt quá 15 ký tự")]
        public string Hotline { get; set; }

        public int EmployeeNumber { get; set; }

        public DateTime OpenDate { get; set; }

        [StringLength(255, ErrorMessage = "Ghi chú không vượt quá 255 ký tự")]
        public string Notes { get; set; }

        public Guid LocationID { get; set; }
        
    }
}
