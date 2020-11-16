using System;
using System.ComponentModel.DataAnnotations;

namespace BillsOfExchange.Contracts
{
    /// <summary>
    /// Požadavek na stránkovaná data
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// Číslo stránky
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "Hodnota stránky musí být od 0 do 2.147.483.647")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Velikost stránky
        /// </summary>
        [Range(1, 100, ErrorMessage = "Velikost stránky je povolena od 1 do 100 řádků.")]
        public int PageSize { get; set; } = 10;
    }
}