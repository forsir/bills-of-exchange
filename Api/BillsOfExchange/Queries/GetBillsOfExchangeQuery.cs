using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Base;
using BillsOfExchange.Models;
using BillsOfExchange.Repositories;

namespace BillsOfExchange.Queries
{
    /// <summary>
    /// Směnky - stránkovaně
    /// </summary>
    public class GetBillsOfExchangeQuery : IGetBillsOfExchangeQuery
    {
        private readonly IBillOfExchangeRepository billOfExchangeRepository;
        private readonly IValidator<Models.BillOfExchange> billOfExchangeValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="billOfExchangeRepository"></param>
        /// <param name="billOfExchangeValidator"></param>
        public GetBillsOfExchangeQuery(
            IBillOfExchangeRepository billOfExchangeRepository,
            IValidator<Models.BillOfExchange> billOfExchangeValidator
        )
        {
            this.billOfExchangeRepository = billOfExchangeRepository;
            this.billOfExchangeValidator = billOfExchangeValidator;
        }

        /// <inheritdoc />
        public async Task<PagedResult<BillOfExchange>> ExecuteAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var countTask = this.billOfExchangeRepository.GetRowsCount(cancellationToken);
            var dataTask = this.billOfExchangeRepository.Get(page, pageSize, cancellationToken);

            await Task.WhenAll(countTask, dataTask);

            var dataResult = new List<Models.BillOfExchange>();

            foreach (var billOfExchange in dataTask.Result)
            {
                billOfExchange.ValidatorResult = this.billOfExchangeValidator.Validate(billOfExchange);
                dataResult.Add(billOfExchange);
            }

            return new PagedResult<BillOfExchange>(countTask.Result, dataResult, page, pageSize);
        }
    }
}