using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Base;
using BillsOfExchange.Models;
using BillsOfExchange.Repositories;

namespace BillsOfExchange.Queries
{
    /// <summary>
    /// Osoby - stránkovaně
    /// </summary>
    public class GetPartiesQuery : IGetPartiesQuery
    {
        private readonly IPartyRepository partyRepository;
        private readonly IValidator<Models.Party> partyValidator;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="partyRepository"></param>
        /// <param name="partyValidator"></param>
        public GetPartiesQuery(
            IPartyRepository partyRepository,
            IValidator<Models.Party> partyValidator
        )
        {
            this.partyRepository = partyRepository;
            this.partyValidator = partyValidator;
        }

        /// <inheritdoc />
        public async Task<PagedResult<Party>> ExecuteAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var countTask = this.partyRepository.GetRowsCount(cancellationToken);
            var dataTask = this.partyRepository.Get(page, pageSize, cancellationToken);

            await Task.WhenAll(countTask, dataTask);

            var dataResult = new List<Models.Party>();

            foreach (var party in dataTask.Result)
            {
                party.ValidatorResult = this.partyValidator.Validate(party);
                dataResult.Add(party);
            }

            return new PagedResult<Party>(countTask.Result, dataResult, page, pageSize);
        }
    }
}