using System;
using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Base;
using BillsOfExchange.Exceptions;
using BillsOfExchange.Models;
using BillsOfExchange.Repositories;

namespace BillsOfExchange.Queries
{
    /// <summary>
    /// Směnka s rubopisy dle ID směnky
    /// </summary>
    public class GetEndorsmentsQuery : IGetEndorsmentsQuery
    {
        private readonly IBillOfExchangeRepository billOfExchangeRepository;
        private readonly IValidator<Models.BillOfExchangeEndorsments> billOfExchangeEndorsmentsValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="billOfExchangeRepository"></param>
        /// <param name="billOfExchangeEndorsmentsValidator"></param>
        public GetEndorsmentsQuery(
            IBillOfExchangeRepository billOfExchangeRepository,
            IValidator<Models.BillOfExchangeEndorsments> billOfExchangeEndorsmentsValidator
        )
        {
            this.billOfExchangeRepository = billOfExchangeRepository;
            this.billOfExchangeEndorsmentsValidator = billOfExchangeEndorsmentsValidator;
        }

        /// <inheritdoc />
        public async Task<BillOfExchangeEndorsments> ExecuteAsync(int billOfExchangeId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var billOfExchange = await this.billOfExchangeRepository.GetById(billOfExchangeId, cancellationToken);

            if (billOfExchange == null)
            {
                throw new NotFoundException($"Směnka s ID = {billOfExchangeId} nebyla nalezena.");
            }

            var result = new BillOfExchangeEndorsments()
            {
                BillOfExchange = billOfExchange
            };

            try
            {
                var endorsments = await this.billOfExchangeRepository.GetEndorsments(billOfExchangeId, cancellationToken);
                result.Endorsments = endorsments;
                result.ValidatorResult = this.billOfExchangeEndorsmentsValidator.Validate(result);
            }
            catch (Exception e)
            {
                result.Endorsments = Array.Empty<Endorsment>();
                result.ValidatorResult = new ValidatorResult();
                result.ValidatorResult.SetError(e.Message);
            }

            return result;
        }
    }
}