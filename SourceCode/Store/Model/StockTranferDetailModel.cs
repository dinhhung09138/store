using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StockTranferDetailModel : Base
    {
        public Guid ID { get; set; }

        public Guid StockTranferID { get; set; }

        public Guid ProductID { get; set; }

        public decimal Number { get; set; }

    }
}
