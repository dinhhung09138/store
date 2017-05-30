using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Stock-in model
    /// </summary>
    public class StockInModel : Base
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
        /// ID
        /// </summary>
        [Required(ErrorMessage = "Chọn Chi nhánh")]
        public Guid BranchID { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "Chọn nhà cung cấp")]
        public Guid SupplierID { get; set; }

        /// <summary>
        /// Stock in date
        /// </summary>
        [Required(ErrorMessage = "Ngày nhập kho không được rỗng")]
        public DateTime StockInDate { get; set; }

        /// <summary>
        /// Total money
        /// </summary>
        public decimal TotalMoney { get; set; } = 0;

        /// <summary>
        /// Discount
        /// </summary>
        public decimal Discount { get; set; } = 0;

        /// <summary>
        /// Payable
        /// </summary>
        public decimal Payable { get; set; } = 0;

        /// <summary>
        /// Dept
        /// </summary>
        public decimal Dept { get; set; } = 0;

        /// <summary>
        /// Employee's id input to stock
        /// </summary>
        [Required(ErrorMessage = "Nhân viên nhập kho không được rỗng")]
        public Guid EmployeeID { get; set; }
        
        /// <summary>
        /// Reason
        /// </summary>
        [StringLength(255, ErrorMessage = "Lý do nhập không vượt quá 255 ký tự")]
        public string Reason { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, ErrorMessage = "Ghi chú không vượt quá 255 ký tự")]
        public string Notes { get; set; }
    }
}
