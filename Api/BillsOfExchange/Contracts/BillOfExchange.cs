using BillsOfExchange.Base;

namespace BillsOfExchange.Contracts
{
    /// <summary>
    /// Bill of Exchange is a security representing amount of something
    /// </summary>
    public class BillOfExchange
    {
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Party that issued the Bill
        /// </summary>
        public Party Drawer { get; set; }

        /// <summary>
        /// Party that was the Bill issued to
        /// </summary>
        public Party Beneficiary { get; set; }

        /// <summary>
        /// Amount of goods the Bill represents
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Output from Validator
        /// </summary>
        public ValidatorResult ValidatorResult { get; set; }
    }
}