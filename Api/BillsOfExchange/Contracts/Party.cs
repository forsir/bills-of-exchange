using BillsOfExchange.Base;

namespace BillsOfExchange.Contracts
{
    /// <summary>
    /// Represents a legal person
    /// </summary>
    public class Party
    {
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full name of the person
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Výstup validátoru
        /// </summary>
        public ValidatorResult ValidatorResult { get; set; }
    }
}