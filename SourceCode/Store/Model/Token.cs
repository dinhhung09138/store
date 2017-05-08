using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// User token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Token string
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Time of issued on
        /// </summary>
        public DateTime IssuedOn { get; set; }

        /// <summary>
        /// Expires on
        /// </summary>
        public DateTime ExpiresOn { get; set; }

    }
}
