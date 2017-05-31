using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccess
{
    public class ContractTypeSrv
    {
        /// <summary>
        /// Get list group of customer to display on dropdown controll (or for something else) in other form
        /// </summary>
        /// <returns></returns>
        public List<ContractTypeModel> GetListForDisplay()
        {
            List<ContractTypeModel> _return = new List<ContractTypeModel>();
            using (var context = new StoreEntities())
            {
                var list = (from a in context.contract_type orderby a.name select new { a.code, a.name }).ToList();
                foreach (var item in list)
                {
                    _return.Add(new ContractTypeModel() { Code = item.code, Name = item.name });
                }
            }
            return _return;
        }
    }
}
