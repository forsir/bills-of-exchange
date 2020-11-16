using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Attributes;
using BillsOfExchange.Models;
using BillsOfExchange.Providers;

namespace BillsOfExchange.Repositories
{
    /// <summary>
    /// Party repository
    /// </summary>
    public class PartyRepository : IPartyRepository
    {
        private readonly IDataSourceProvider dataSourceProvider;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dataSourceProvider"></param>
        public PartyRepository(
            IDataSourceProvider dataSourceProvider
        )
        {
            this.dataSourceProvider = dataSourceProvider;
        }


        /// <inheritdoc />
        [LogMethod]
        public async Task<int> GetRowsCount(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return 0;
            }

            var ds = await this.dataSourceProvider.PartiesDataSource(cancellationToken);
            return ds.Count;
        }


        /// <inheritdoc />
        [LogMethod]
        public async Task<IEnumerable<Party>> Get(int page, int pageSize, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return Enumerable.Empty<Party>();
            }

            var ds = await this.dataSourceProvider.PartiesDataSource(cancellationToken);
            return ds.Data.Skip((page - 1) * pageSize).Take(pageSize);
        }


        /// <inheritdoc />
        [LogMethod]
        public async Task<Party> GetById(int partyId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return null;
            }

            var ds = await this.dataSourceProvider.PartiesDataSource(cancellationToken);
            var parties = ds.Data.Where(t => t.Id == partyId);

            if (parties.Count() < 2)
            {
                return ds.Data.SingleOrDefault(t => t.Id == partyId);
            }

            throw new InvalidOperationException($"V datech se nachází více osob s ID = {partyId} (celkem {parties.Count()}).");
        }
    }
}