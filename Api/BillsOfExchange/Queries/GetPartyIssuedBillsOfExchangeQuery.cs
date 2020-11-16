using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Base;
using BillsOfExchange.Exceptions;
using BillsOfExchange.Models;
using BillsOfExchange.Repositories;

namespace BillsOfExchange.Queries
{
    /// <summary>
    /// Směnky, které vydala osoba dle ID osoby
    /// </summary>
    public class GetPartyIssuedBillsOfExchangeQuery : IGetPartyIssuedBillsOfExchangeQuery
    {
        private readonly IPartyRepository partyRepository;
        private readonly IBillOfExchangeRepository billOfExchangeRepository;
        private readonly IValidator<Models.PartyBillsOfExchange> partyBillsOfExchangeValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="partyRepository"></param>
        /// <param name="billOfExchangeRepository"></param>
        /// <param name="partyBillsOfExchangeValidator"></param>
        public GetPartyIssuedBillsOfExchangeQuery(
            IPartyRepository partyRepository,
            IBillOfExchangeRepository billOfExchangeRepository,
            IValidator<Models.PartyBillsOfExchange> partyBillsOfExchangeValidator
        )
        {
            this.partyRepository = partyRepository;
            this.billOfExchangeRepository = billOfExchangeRepository;
            this.partyBillsOfExchangeValidator = partyBillsOfExchangeValidator;
        }

        /// <inheritdoc />
        public async Task<PartyBillsOfExchange> ExecuteAsync(int partyId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var party = await this.partyRepository.GetById(partyId, cancellationToken);

            if (party == null)
            {
                throw new NotFoundException($"Osoba s ID = {partyId} nebyla nalezena.");
            }

            var result = new PartyBillsOfExchange()
            {
                Party = party
            };

            var billsOfExchange = await this.billOfExchangeRepository.GetIssuedByPartyId(partyId, cancellationToken);

            result.BillsOfExchange = billsOfExchange;

            result.ValidatorResult = this.partyBillsOfExchangeValidator.Validate(result);

            return result;
        }
    }
}