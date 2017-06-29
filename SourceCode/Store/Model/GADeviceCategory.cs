using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// GA device category
    /// </summary>
    public class GADeviceCategory : GAAccount
    {
        /// <summary>
        /// Device's name
        /// </summary>
        public string Device { get; set; }
    }
}
