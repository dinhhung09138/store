using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Google analystic account
    /// </summary>
    public class GAAccount : Base
    {
        /// <summary>
        /// The identify of item
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// GA view's id
        /// </summary>
        public string ViewID { get; set; }

        /// <summary>
        /// GA view's name
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// GA property's id
        /// </summary>
        public string PropertyID { get; set; }

        /// <summary>
        /// GA property's name
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// GA account's id
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// GA account's name
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// GA table's id
        /// </summary>
        public string TableID { get; set; }

        /// <summary>
        /// Day
        /// </summary>
        public int Day { get; set; }
        
        /// <summary>
        /// Month
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// New users
        /// </summary>
        public int NewUsers { get; set; }

        /// <summary>
        /// Return users
        /// </summary>
        public int ReturnUsers { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        public int Users { get; set; }

        /// <summary>
        /// Session
        /// </summary>
        public int Sessions { get; set; }

        /// <summary>
        /// Bounces
        /// </summary>
        public float Bounces { get; set; }

        /// <summary>
        /// Bounces rate
        /// </summary>
        public float Bouncerate { get; set; }

        /// <summary>
        /// Entrances
        /// </summary>
        public float Entrances { get; set; }

        /// <summary>
        /// Pageviews
        /// </summary>
        public float PageViews { get; set; }

        /// <summary>
        /// Page per session
        /// </summary>
        public float PagePerSession { get; set; }

        /// <summary>
        /// Entrance rate
        /// </summary>
        public float EntranceRate { get; set; }
        
        /// <summary>
        /// Unique page views
        /// </summary>
        public float UniquePageViews { get; set; }

        /// <summary>
        /// Time on page
        /// </summary>
        public float TimeOnPage { get; set; }

        /// <summary>
        /// Avg time on page
        /// </summary>
        public float AvgTimeOnPage { get; set; }

        /// <summary>
        /// Exits
        /// </summary>
        public float Exits { get; set; }

        /// <summary>
        /// Exit rate
        /// </summary>
        public float ExitsRate { get; set; }
        
    }
}
