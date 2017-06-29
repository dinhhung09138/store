using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// GA browser model
    /// </summary>
    public class GABrowser : GAAccount
    {
        /// <summary>
        /// Browser
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// Browser version
        /// </summary>
        public string BrowserVersion { get; set; }
    }
}
