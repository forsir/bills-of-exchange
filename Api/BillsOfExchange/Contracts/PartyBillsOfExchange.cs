using System.Collections.Generic;
using BillsOfExchange.Base;

namespace BillsOfExchange.Contracts
{
    /// <summary>
    /// Směnky osoby
    /// </summary>
    public class PartyBillsOfExchange
    {
        /// <summary>
        /// Osoba
        /// </summary>
        public Party Party { get; set; }

        /// <summary>
        /// Směnky
        /// </summary>
        public IEnumerable<BillOfExchange> BillsOfExchange { get; set; }

        /// <summary>
        /// Výstup validátoru
        /// </summary>
        public ValidatorResult ValidatorResult { get; set; }
    }
}