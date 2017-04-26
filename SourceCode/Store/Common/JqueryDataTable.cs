using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.JqueryDataTable
{
    /// <summary>
    /// List of column of datatable
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Name of column map with column's name in data object
        /// IF not, that is the order of the column in table on design
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Allow for Search
        /// </summary>
        public bool Searchable { get; set; }
        /// <summary>
        /// Allow for order
        /// </summary>
        public bool Orderable { get; set; }

        public Column()
        {
            this.Data = "";
            this.Name = "";
            this.Searchable = false;
            this.Orderable = false;
        }
    }

    public static class JqueryDataTableCollectionHelper
    {
        /// <summary>
        /// Order list
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="items"></param>
        /// <param name="dir"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<TSource> Sort<TSource, TKey>(this IEnumerable<TSource> items, SortingDirection dir, Func<TSource, TKey> keySelector)
        {
            if (dir == SortingDirection.Asc)
            {
                return items.OrderBy(keySelector);
            }
            return items.OrderByDescending(keySelector);
        }

        /// <summary>
        /// Based on order on query string. Check and set name for columns are ordering
        /// Use for check sorting.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static JqueryDataTableRequest SetOrderingColumnName(this JqueryDataTableRequest item)
        {
            for (int i = 0; i < item.order.Count; i++)
            {
                item.order[i].ColumnName = item.columns[item.order[i].Column].Data;
            }
            return item;
        }

        /// <summary>
        /// Based on order on query string. Check and set name for columns are ordering
        /// Use for check sorting.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static CustomJqueryDataTableRequest SetOrderingColumnName(this CustomJqueryDataTableRequest item)
        {
            for (int i = 0; i < item.order.Count; i++)
            {
                item.order[i].ColumnName = item.columns[item.order[i].Column].Data;
            }
            return item;
        }
    }

    /// <summary>
    /// Jquery datatable Request from client
    /// </summary>
    public class JqueryDataTableRequest
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Ordering> order { get; set; }
        public Search search { get; set; }
        public List<Column> columns { get; set; }
    }

    /// <summary>
    /// JqueryData response to client
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JqueryDataTableResponse<T> where T : class
    {
        /// <summary>
        /// Draw counter. 
        /// This is used by DataTables to ensure that the Ajax returns from server-side processing 
        /// requests are drawn in sequence by DataTables (Ajax requests are asynchronous and thus can return out of sequence). 
        /// This is used as part of the draw return parameter.
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// Sets the Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        public long recordsTotal { get; set; }

        /// <summary>
        /// Sets the Total records, after filtering 
        /// (i.e. the total number of records after filtering has been applied - 
        /// not just the number of records being returned in this result set)
        /// </summary>
        public long recordsFiltered { get; set; }

        /// <summary>
        /// Data return
        /// </summary>
        public List<T> data { get; set; }
    }

    /// <summary>
    /// Ordering table
    /// </summary>
    public class Ordering
    {
        /// <summary>
        /// order column on table
        /// </summary>
        public int Column { get; set; }
        /// <summary>
        /// Custom property
        /// Set column name for the current ordering column
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// Direction
        /// </summary>
        public SortingDirection Dir { get; set; }
    }

    /// <summary>
    /// Search content
    /// </summary>
    public class Search
    {
        /// <summary>
        /// String value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Regex { get; set; }
    }

    public enum SortingDirection
    {
        Asc,
        Desc
    }

    /// <summary>
    /// Use for some cases user want to have some variable included into parameter of jquery datatable
    /// </summary>
    public class CustomJqueryDataTableRequest : JqueryDataTableRequest
    {
        /// <summary>
        /// Id of column you want to send
        /// </summary>
        public int ID { get; set; }
        public string Url { get; set; }
        public string TableName { get; set; }
        public string ModuleCode { get; set; }
        public string Filter { get; set; }

        public CustomJqueryDataTableRequest()
        {
            this.ID = 0;
            this.TableName = string.Empty;
            this.ModuleCode = string.Empty;
            this.Filter = string.Empty;
        }
    }

}
