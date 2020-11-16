using System.Linq;
using BillsOfExchange.Base;
using BillsOfExchange.Models;

namespace BillsOfExchange.Validators
{
    /// <summary>
    /// Validátor BillOfExchangeEndorsments
    /// </summary>
    public class BillOfExchangeEndorsmentsValidator : IValidator<Models.BillOfExchangeEndorsments>
    {
        private readonly IValidator<Models.BillOfExchange> billsOfExchangeValidator;
        private readonly IValidator<Models.Party> partyValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="billsOfExchangeValidator"></param>
        /// <param name="partyValidator"></param>
        public BillOfExchangeEndorsmentsValidator(
            IValidator<Models.BillOfExchange> billsOfExchangeValidator,
            IValidator<Models.Party> partyValidator
        )
        {
            this.billsOfExchangeValidator = billsOfExchangeValidator;
            this.partyValidator = partyValidator;
        }

        /// <inheritdoc />
        public ValidatorResult Validate(BillOfExchangeEndorsments objectToValidate)
        {
            var result = new ValidatorResult();

            if (objectToValidate.Endorsments == null || !objectToValidate.Endorsments.Any())
            {
                return result;
            }

            var endorsments = objectToValidate.Endorsments.Reverse().ToArray();

            if (endorsments[0].NewBeneficiaryId == objectToValidate.BillOfExchange.BeneficiaryId)
            {
                result.SetError($"Směnka s ID = {objectToValidate.BillOfExchange.Id} je prvním rubopisem převedena stejnému majiteli.");
            }

            for (int i = 0; i < (endorsments.Length - 1); i++)
            {
                if (endorsments[i].NewBeneficiaryId == endorsments[i + 1].NewBeneficiaryId)
                {
                    result.SetError($"Směnka s ID = {objectToValidate.BillOfExchange.Id} je rubopisy ID = {endorsments[i].Id} a ID = {endorsments[i + 1].Id} převáděna stejnému majiteli.");
                }

                if (endorsments[i].NewBeneficiary != null)
                {
                    endorsments[i].NewBeneficiary.ValidatorResult = this.partyValidator.Validate(endorsments[i].NewBeneficiary);
                }
            }

            var unknownBeneficiary = endorsments.Where(t => t.NewBeneficiary == null);

            if (unknownBeneficiary.Any())
            {
                result.SetError($"Směnka s ID = {objectToValidate.BillOfExchange.Id} obsahuje rubopisy {string.Join(' ', unknownBeneficiary.Select(t => $"ID = {t.Id}"))} s neznámým majitelem.");
            }


            objectToValidate.BillOfExchange.ValidatorResult = this.billsOfExchangeValidator.Validate(objectToValidate.BillOfExchange);

            return result;
        }
    }
}