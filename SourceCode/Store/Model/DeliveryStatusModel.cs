using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Delivery status
    /// </summary>
    public class DeliveryStatusModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID không được rỗng")]
        public byte ID { get; set; }
        
        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "Tên không được rỗng")]
        [StringLength(50, ErrorMessage = "Tên không vượt quá 50 ký tự")]
        public string Name { get; set; }

    }
}
