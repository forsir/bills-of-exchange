using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Base;
using BillsOfExchange.Extensions;
using BillsOfExchange.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.RequestHandlers
{
    /// <summary>
    /// Směnky, které vydala osoba dle ID osoby
    /// </summary>
    public class GetPartyIssuedBillsOfExchangeRequestHandler : IGetPartyIssuedBillsOfExchangeRequestHandler
    {
        private readonly IGetPartyIssuedBillsOfExchangeQuery getPartyIssuedBillsOfExchangeQuery;
        private readonly IMapper<Models.PartyBillsOfExchange, Contracts.PartyBillsOfExchange> modelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="getPartyIssuedBillsOfExchangeQuery"></param>
        /// <param name="modelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper"></param>
        public GetPartyIssuedBillsOfExchangeRequestHandler(
            IGetPartyIssuedBillsOfExchangeQuery getPartyIssuedBillsOfExchangeQuery,
            IMapper<Models.PartyBillsOfExchange, Contracts.PartyBillsOfExchange> modelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper
        )
        {
            this.getPartyIssuedBillsOfExchangeQuery = getPartyIssuedBillsOfExchangeQuery;
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

            var queryResult = await this.getPartyIssuedBillsOfExchangeQuery.ExecuteAsync(partyId, cancellationToken);

            var result = this.modelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper.Map(queryResult);

            return new OkObjectResult(result);
        }
    }
}