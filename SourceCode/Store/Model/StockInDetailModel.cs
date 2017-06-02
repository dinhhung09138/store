using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class StockInDetailModel : Base
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Stock int ID
        /// </summary>
        [Required(ErrorMessage = "ID Phiếu nhập kho không được rỗng")]
        public Guid StockInID { get; set; }

        /// <summary>
        /// Good's id
        /// </summary>
        [Required(ErrorMessage = "ID sản phẩm không được rỗng")]
        public Guid GoodsID { get; set; }

        /// <summary>
        /// Goods's name
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// Output number
        /// </summary>
        [Range(0.1, 99999999, ErrorMessage = "Số lượng nhập phải lớn hơn 0")]
        public decimal Number { get; set; } = 0;

        /// <summary>
        /// Output number
        /// </summary>
        [Range(0.1, 999999999, ErrorMessage = "Giá nhập phải lớn hơn 0")]
        public decimal Price { get; set; } = 0;

    }
}
