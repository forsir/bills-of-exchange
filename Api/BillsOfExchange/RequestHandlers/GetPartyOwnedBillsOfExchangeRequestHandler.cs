using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Base;
using BillsOfExchange.Extensions;
using BillsOfExchange.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.RequestHandlers
{
    /// <summary>
    /// Směnky, které osoba vlastní dle ID osoby
    /// </summary>
    public class GetPartyOwnedBillsOfExchangeRequestHandler : IGetPartyOwnedBillsOfExchangeRequestHandler
    {
        private readonly IGetPartyOwnedBillsOfExchangeQuery getPartyOwnedBillsOfExchangeQuery;
        private readonly IMapper<Models.PartyBillsOfExchange, Contracts.PartyBillsOfExchange> modelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="getPartyOwnedBillsOfExchangeQuery"></param>
        /// <param name="modelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper"></param>
        public GetPartyOwnedBillsOfExchangeRequestHandler(
            IGetPartyOwnedBillsOfExchangeQuery getPartyOwnedBillsOfExchangeQuery,
            IMapper<Models.PartyBillsOfExchange, Contracts.PartyBillsOfExchange> modelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper
        )
        {
            this.getPartyOwnedBillsOfExchangeQuery = getPartyOwnedBillsOfExchangeQuery;
            this.modelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper = modelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper;
        }

        /// <inheritdoc />
        public async Task<IActionResult> ExecuteAsync(int partyId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return null;
            }

            var queryResult = await this.getPartyOwnedBillsOfExchangeQuery.ExecuteAsync(partyId, cancellationToken);

            var result = this.modelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper.Map(queryResult);

            return new OkObjectResult(result);
        }
    }
}