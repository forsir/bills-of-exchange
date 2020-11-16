using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Base;
using BillsOfExchange.Extensions;
using BillsOfExchange.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.RequestHandlers
{
    /// <summary>
    /// Směnka s rubopisy dle ID směnky
    /// </summary>
    public class GetEndorsmentsRequestHandler : IGetEndorsmentsRequestHandler
    {
        private readonly IGetEndorsmentsQuery getEndorsmentsQuery;
        private readonly IMapper<Models.BillOfExchangeEndorsments, Contracts.BillOfExchangeEndorsments> modelBillOfExchangeEndorsmentsToContractBillOfExchangeEndorsmentsMapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="getEndorsmentsQuery"></param>
        /// <param name="modelBillOfExchangeEndorsmentsToContractBillOfExchangeEndorsmentsMapper"></param>
        public GetEndorsmentsRequestHandler(
            IGetEndorsmentsQuery getEndorsmentsQuery,
            IMapper<Models.BillOfExchangeEndorsments, Contracts.BillOfExchangeEndorsments> modelBillOfExchangeEndorsmentsToContractBillOfExchangeEndorsmentsMapper
        )
        {
            this.getEndorsmentsQuery = getEndorsmentsQuery;
            this.modelBillOfExchangeEndorsmentsToContractBillOfExchangeEndorsmentsMapper = modelBillOfExchangeEndorsmentsToContractBillOfExchangeEndorsmentsMapper;
        }

        /// <inheritdoc />
        public async Task<IActionResult> ExecuteAsync(int billOfExchangeId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return null;
            }

            var queryResult = await this.getEndorsmentsQuery.ExecuteAsync(billOfExchangeId, cancellationToken);

            var result = this.modelBillOfExchangeEndorsmentsToContractBillOfExchangeEndorsmentsMapper.Map(queryResult);

            return new OkObjectResult(result);
        }
    }
}