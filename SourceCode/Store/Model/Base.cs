using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Base
    {
        /// <summary>
        /// User creates item
        /// </summary>
        [Required(ErrorMessage = "Nhân viên tạo dữ liệu không được rỗng")]
        public Guid CreateBy { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        [Required(ErrorMessage = "Ngày tạo không được rỗng")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// User updates item
        /// </summary>
        public Guid UpdatedBy { get; set; }

        /// <summary>
        /// Time update item
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Deleted status
        /// </summary>
        [Required]
        public bool Deleted { get; set; }

        /// <summary>
        /// User deleted item
        /// </summary>
        public Guid DeletedBy { get; set; }

        /// <summary>
        /// Time when delete
        /// </summary>
        public DateTime DeletedDate { get; set; }

        /// <summary>
        /// true: Insert, false: update
        /// </summary>
        public bool Insert { get; set; }
    }
}
