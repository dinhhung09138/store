using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Location model
    /// </summary>
    public class LocationModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID không được rỗng")]
        public Guid ID { get; set; }

        /// <summary>
        /// Name of branch
        /// </summary>
        [Required(ErrorMessage = "Tên khu vực không được rỗng")]
        [StringLength(150, ErrorMessage = "Tên khu vực không vượt quá 150 ký tự")]
        public string Name { get; set; }
    }
}
