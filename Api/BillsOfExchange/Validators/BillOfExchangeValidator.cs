using BillsOfExchange.Base;

namespace BillsOfExchange.Validators
{
    /// <summary>
    /// Validátor BillOfExchange
    /// </summary>
    public class BillOfExchangeValidator : IValidator<Models.BillOfExchange>
    {
        private readonly IValidator<Models.Party> partyValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="partyValidator"></param>
        public BillOfExchangeValidator(
            IValidator<Models.Party> partyValidator
        )
        {
            this.partyValidator = partyValidator;
        }

        /// <inheritdoc />
        public ValidatorResult Validate(Models.BillOfExchange objectToValidate)
        {
            var result = new ValidatorResult();

            if (objectToValidate.Beneficiary == null)
            {
                result.SetError("Příjemce neexistuje.");
            }
            else
            {
                objectToValidate.Beneficiary.ValidatorResult = this.partyValidator.Validate(objectToValidate.Beneficiary);
            }

            if (objectToValidate.Drawer == null)
            {
                result.SetError("Vystavitel neexistuje.");
            }
            else
            {
                objectToValidate.Drawer.ValidatorResult = this.partyValidator.Validate(objectToValidate.Drawer);
            }

            if (objectToValidate.DrawerId == objectToValidate.BeneficiaryId)
            {
                result.SetError("Příjemce a vystavitel je stejná osoba.");
            }


            return result;
        }
    }
}