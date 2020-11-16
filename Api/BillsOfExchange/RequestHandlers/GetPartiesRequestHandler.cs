using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Base;
using BillsOfExchange.Contracts;
using BillsOfExchange.Extensions;
using BillsOfExchange.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.RequestHandlers
{
    /// <summary>
    /// Osoby - stránkovaně
    /// </summary>
    public class GetPartiesRequestHandler : IGetPartiesRequestHandler
    {
        private readonly IGetPartiesQuery getPartiesQuery;
        private readonly IMapper<Models.Party, Contracts.Party> modelPartyToContractPartyMapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="getPartiesQuery"></param>
        /// <param name="modelPartyToContractPartyMapper"></param>
        public GetPartiesRequestHandler(
            IGetPartiesQuery getPartiesQuery,
            IMapper<Models.Party, Contracts.Party> modelPartyToContractPartyMapper
        )
        {
            this.getPartiesQuery = getPartiesQuery;
            this.modelPartyToContractPartyMapper = modelPartyToContractPartyMapper;
        }

        /// <inheritdoc />
        public async Task<IActionResult> ExecuteAsync(PageRequest pageRequest, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return null;
            }

            var queryResult = await this.getPartiesQuery.ExecuteAsync(pageRequest.Page, pageRequest.PageSize, cancellationToken);

            var data = this.modelPartyToContractPartyMapper.MapList(queryResult.Result);

            var result = new PagedResult<Contracts.Party>(queryResult.TotalRowCount, data, queryResult.CurrentPage, queryResult.PageSize);

            return new OkObjectResult(result);
        }
    }
}