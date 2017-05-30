﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// custom goods model
    /// </summary>
    public class GoodsModel : Base
    {
        /// <summary>
        /// ID of product
        /// </summary>
        [Required(ErrorMessage = "ID hàng hóa không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Code of product
        /// </summary>
        [Required(ErrorMessage = "Mã hàng hóa không được rỗng")]
        [StringLength(20, ErrorMessage = "Mã hàng hóa không vượt quá 20 ký tự")]
        public string Code { get; set; }

        /// <summary>
        /// Name of product
        /// </summary>
        [Required(ErrorMessage = "Tên không được rỗng")]
        [StringLength(50, ErrorMessage = "Tên không vượt quá 50 ký tự")]
        public string Name { get; set; }

        /// <summary>
        /// Unit's id
        /// </summary>
        public Guid? UnitID { get; set; }

        /// <summary>
        /// Unit's name
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Product group's id
        /// </summary>
        public Guid? GroupID { get; set; }

        /// <summary>
        /// Product group's name
        /// </summary>
        public string GroupName { get; set; }
        
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Original price
        /// </summary>
        public decimal OriPrice { get; set; }

        /// <summary>
        /// Number of the rest product in stock
        /// </summary>
        [Range(0, 9999999.9, ErrorMessage = "Số lượng tồn từ 0 đến 999999999.9")]
        public decimal NumInStock { get; set; }

        /// <summary>
        /// Weight on a unit of product
        /// </summary>
        [Range(0, 9999999.9, ErrorMessage = "Cân nặng từ 0 đến 999999999.9")]
        public decimal Weight { get; set; }

        /// <summary>
        /// Allow sale direct
        /// </summary>
        public bool AllowSaleDirect { get; set; }

        /// <summary>
        /// Picture 01
        /// </summary>
        [StringLength(100, ErrorMessage = "Đường dẫn ảnh không vượt quá 100 ký tự")]
        public string Avatar { get; set; }
        
        /// <summary>
        /// Mininum value in stock
        /// </summary>
        [Range(0, 9999999.9, ErrorMessage = "Số lượng tồn tối thiểu từ 0 đến 999999999.9")]
        public decimal MinInStock { get; set; }

        /// <summary>
        /// Maximum value in stock
        /// </summary>
        [Range(0, 9999999.9, ErrorMessage = "Số lượng tồn tối đa từ 0 đến 999999999.9")]
        public decimal MaxInStock { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Notes in order form
        /// </summary>
        [StringLength(50, ErrorMessage = "Ghi chú không vượt quá 50 ký tự")]
        public string NoteInOrder { get; set; }
    }
}
