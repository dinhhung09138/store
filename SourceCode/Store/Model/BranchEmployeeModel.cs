using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// Branch employee model
    /// </summary>
    public class BranchEmployeeModel : Base
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
        /// Employee's id
        /// </summary>
        [Required(ErrorMessage = "Nhân viên không được rỗng")]
        public Guid EmplID { get; set; }

        /// <summary>
        /// Employee's name
        /// </summary>
        public Guid EmplName { get; set; }

        /// <summary>
        /// Position's code
        /// </summary>
        public string PositionCode { get; set; }

        /// <summary>
        /// Position's name
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Department's code
        /// </summary>
        public Guid DeptCode { get; set; }

        /// <summary>
        /// Department's name
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// Start working date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End of work
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [StringLength(255, ErrorMessage = "Ghi chú không vượt quá 255 ký tự")]
        public string Notes { get; set; }
    }
}
