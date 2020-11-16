using System.Collections.Generic;
using System.Linq;
using BillsOfExchange.Base;
using BillsOfExchange.Models;

namespace BillsOfExchange.Validators
{
    /// <summary>
    /// Validátor PartyBillsOfExchange
    /// </summary>
    public class PartyBillsOfExchangeValidator : IValidator<Models.PartyBillsOfExchange>
    {
        private readonly IValidator<Models.BillOfExchange> billsOfExchangeValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="billsOfExchangeValidator"></param>
        public PartyBillsOfExchangeValidator(
            IValidator<Models.BillOfExchange> billsOfExchangeValidator
        )
        {
            this.billsOfExchangeValidator = billsOfExchangeValidator;
        }

        /// <inheritdoc />
        public ValidatorResult Validate(PartyBillsOfExchange objectToValidate)
        {
            var result = new ValidatorResult();

            if (objectToValidate.BillsOfExchange == null || !objectToValidate.BillsOfExchange.Any())
            {
                return result;
            }

            var billsOfExchange = new List<BillOfExchange>();

            foreach (var billOfExchange in objectToValidate.BillsOfExchange)
            {
                billOfExchange.ValidatorResult = this.billsOfExchangeValidator.Validate(billOfExchange);
                billsOfExchange.Add(billOfExchange);
            }

            objectToValidate.BillsOfExchange = billsOfExchange;

            return result;
        }
    }
}