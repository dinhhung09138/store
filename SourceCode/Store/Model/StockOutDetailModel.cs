using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Stock-out detail model
    /// </summary>
    public class StockOutDetailModel : Base
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Stock Out ID
        /// </summary>
        [Required(ErrorMessage = "ID Phiếu xuất kho không được rỗng")]
        public Guid StockOutID { get; set; }

        /// <summary>
        /// Product's id
        /// </summary>
        [Required(ErrorMessage = "ID sản phẩm không được rỗng")]
        public Guid GoodsID { get; set; }

        /// <summary>
        /// Product's name
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// Branch's id
        /// </summary>
        public Guid BranchID { get; set; }

        /// <summary>
        /// Output number
        /// </summary>
        [Range(0.1, 99999999, ErrorMessage = "Số lượng xuất phải lớn hơn 0")]
        public decimal Number { get; set; } = 0;

        /// <summary>
        /// Output number
        /// </summary>
        [Range(0.1, 999999999, ErrorMessage = "Giá xuất phải lớn hơn 0")]
        public decimal Price { get; set; } = 0;

    }
}
