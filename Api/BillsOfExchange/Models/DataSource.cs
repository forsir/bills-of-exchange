using System.Collections.Generic;

namespace BillsOfExchange.Models
{
    /// <summary>
    /// Zdroj dat
    /// </summary>
    /// <typeparam name="T">Typ dat</typeparam>
    public class DataSource<T> where T : class
    {
        /// <summary>
        /// Celkový počet
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}