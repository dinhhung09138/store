using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Inventory model
    /// </summary>
    public class InventoryModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Branch's id
        /// </summary>
        [Required(ErrorMessage = "Chi nhánh không được rỗng")]
        public Guid BranchID { get; set; }

        /// <summary>
        /// Branch's name
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// Product's id
        /// </summary>
        [Required(ErrorMessage = "Sản phẩm không được rỗng")]
        public Guid ProductID { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// Tổng tồn
        /// </summary>
        public float Total { get; set; } = 0;

        /// <summary>
        /// The lastest update
        /// </summary>
        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}
