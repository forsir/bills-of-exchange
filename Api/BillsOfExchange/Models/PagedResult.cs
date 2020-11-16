using System.Collections.Generic;

namespace BillsOfExchange.Models
{
    /// <summary>
    /// Stránka dat
    /// </summary>
    /// <typeparam name="T">Typ dat na stránce</typeparam>
    public class PagedResult<T> where T : class
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="totalRowCount">Celkový počet objektů</param>
        /// <param name="result">Pole objektů</param>
        /// <param name="currentPage">Číslo stránky, která je součástí tohoto objktu</param>
        /// <param name="pageSize">Velikost stránky</param>
        public PagedResult(int totalRowCount, IEnumerable<T> result, int currentPage, int pageSize)
        {
            this.TotalRowCount = totalRowCount;
            this.Result = result;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalPageCount = ((this.TotalRowCount - 1) / this.PageSize) + 1;
        }

        /// <summary>
        /// Celkový počet objektů
        /// </summary>
        public int TotalRowCount { get; set; }

        /// <summary>
        /// Pole objektů
        /// </summary>
        public IEnumerable<T> Result { get; set; }

        /// <summary>
        /// Číslo stránky, která je součástí tohoto objktu
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Velikost stránky
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Celkový počet stránek
        /// </summary>
        public int TotalPageCount { get; set; }
    }
}