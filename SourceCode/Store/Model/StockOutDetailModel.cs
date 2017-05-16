using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StockOutDetailModel : Base
    {
        public Guid ID { get; set; }

        public Guid StockOutID { get; set; }

        public Guid ProductID { get; set; }

        public decimal Number { get; set; }

        public decimal Price { get; set; }

    }
}
