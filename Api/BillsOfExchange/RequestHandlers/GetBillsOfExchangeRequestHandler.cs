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
    /// Směnky - stránkovaně
    /// </summary>
    public class GetBillsOfExchangeRequestHandler : IGetBillsOfExchangeRequestHandler
    {
        private readonly IGetBillsOfExchangeQuery getBillsOfExchangeQuery;
        private readonly IMapper<Models.BillOfExchange, Contracts.BillOfExchange> modelBillOfExchangeToContractBillOfExchangeMapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="getBillsOfExchangeQuery"></param>
        /// <param name="modelBillOfExchangeToContractBillOfExchangeMapper"></param>
        public GetBillsOfExchangeRequestHandler(
            IGetBillsOfExchangeQuery getBillsOfExchangeQuery,
            IMapper<Models.BillOfExchange, Contracts.BillOfExchange> modelBillOfExchangeToContractBillOfExchangeMapper
        )
        {
            this.getBillsOfExchangeQuery = getBillsOfExchangeQuery;
            this.modelBillOfExchangeToContractBillOfExchangeMapper = modelBillOfExchangeToContractBillOfExchangeMapper;
        }

        /// <inheritdoc />
        public async Task<IActionResult> ExecuteAsync(PageRequest pageRequest, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return null;
            }

            var queryResult = await this.getBillsOfExchangeQuery.ExecuteAsync(pageRequest.Page, pageRequest.PageSize, cancellationToken);

            var data = this.modelBillOfExchangeToContractBillOfExchangeMapper.MapList(queryResult.Result);

            var result = new PagedResult<Contracts.BillOfExchange>(queryResult.TotalRowCount, data, queryResult.CurrentPage, queryResult.PageSize);

            return new OkObjectResult(result);
        }
    }
}