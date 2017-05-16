using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StockTranferModel : Base
    {
        public Guid ID { get; set; }

        public string Code { get; set; }

        public Guid BranchFromID { get; set; }

        public Guid BranchToID { get; set; }

        public DateTime StockTranferDate { get; set; }
        
        public Guid EmployeeID { get; set; }

        public string Reason { get; set; }

        public string Notes { get; set; }
    }
}
