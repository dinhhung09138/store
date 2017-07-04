using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// GA user by city
    /// </summary>
    public class GAUserCity
    {
        /// <summary>
        /// The identify of item
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// GA table's id
        /// </summary>
        public string TableID { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Day
        /// </summary>
        public byte Day { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        public byte Month { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        public short Year { get; set; }

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
