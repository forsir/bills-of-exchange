using System.Collections.Generic;
using BillsOfExchange.Base;

namespace BillsOfExchange.Models
{
    /// <summary>
    /// Seřazené rubopisy směnky
    /// </summary>
    public class BillOfExchangeEndorsments
    {
        /// <summary>
        /// Směnka
        /// </summary>
        public BillOfExchange BillOfExchange { get; set; }

        /// <summary>
        /// Rubopisy
        /// </summary>
        public IEnumerable<Endorsment> Endorsments { get; set; }

        /// <summary>
        /// Výsup validátoru
        /// </summary>
        public ValidatorResult ValidatorResult { get; set; }
    }
}