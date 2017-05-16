using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class StockOutModel : Base
    {
        public Guid ID { get; set; }

        public string Code { get; set; }

        public Guid BranchID { get; set; }
        
        public DateTime StockInDate { get; set; }

        public decimal TotalMoney { get; set; }

        public decimal Discount { get; set; }

        public decimal Payable { get; set; }

        public decimal Dept { get; set; }

        public Guid EmployeeID { get; set; }

        public string Reason { get; set; }

        public string Notes { get; set; }
    }
}
