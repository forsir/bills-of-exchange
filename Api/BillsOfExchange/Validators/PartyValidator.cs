using BillsOfExchange.Base;

namespace BillsOfExchange.Validators
{
    /// <summary>
    /// Validátor Party
    /// </summary>
    public class PartyValidator : IValidator<Models.Party>
    {
        /// <inheritdoc />
        public ValidatorResult Validate(Models.Party objectToValidate)
        {
            var result = new ValidatorResult();

            if (string.IsNullOrEmpty(objectToValidate.Name?.Trim()))
            {
                result.SetError("Osoba nemá vyplněné jméno.");
            }

            return result;
        }
    }
}