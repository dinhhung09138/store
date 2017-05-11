using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BranchEmployeeModel : Base
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID KH không được rỗng")]
        public Guid ID { get; set; }
        public Guid BranchID { get; set; }
        public string BranchName { get; set; }
        public Guid EmplID { get; set; }
        public Guid EmplName { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public Guid DeptCode { get; set; }
        public string DeptName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
    }
}
