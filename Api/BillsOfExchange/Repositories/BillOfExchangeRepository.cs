using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Attributes;
using BillsOfExchange.Extensions;
using BillsOfExchange.Models;
using BillsOfExchange.Providers;

namespace BillsOfExchange.Repositories
{
    /// <summary>
    /// BillOfExchange Repository
    /// </summary>
    public class BillOfExchangeRepository : IBillOfExchangeRepository
    {
        private readonly IDataSourceProvider dataSourceProvider;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dataSourceProvider"></param>
        public BillOfExchangeRepository(
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

            var ds = await this.dataSourceProvider.BillsOfExchangeDataSource(cancellationToken);
            return ds.Count;
        }

        /// <inheritdoc />
        [LogMethod]
        public async Task<IEnumerable<BillOfExchange>> Get(int page, int pageSize, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return Enumerable.Empty<BillOfExchange>();
            }

            var ds = await this.dataSourceProvider.BillsOfExchangeDataSource(cancellationToken);
            return ds.Data.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <inheritdoc />
        [LogMethod]
        public async Task<BillOfExchange> GetById(int billOfExchangeId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return null;
            }


            var ds = await this.dataSourceProvider.BillsOfExchangeDataSource(cancellationToken);
            var billsOfExchange = ds.Data.Where(t => t.Id == billOfExchangeId);

            if (billsOfExchange.Count() < 2)
            {
                return ds.Data.SingleOrDefault(t => t.Id == billOfExchangeId);
            }

            throw new InvalidOperationException($"V datech se nachází více směnek s ID = {billOfExchangeId} (celkem {billsOfExchange.Count()}).");
        }

        /// <inheritdoc />
        [LogMethod]
        public async Task<IEnumerable<Endorsment>> GetEndorsments(int billOfExchangeId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return Enumerable.Empty<Endorsment>();
            }

            var ds = await this.dataSourceProvider.EndorsmentsDataSource(cancellationToken);
            var results = ds.Data.Where(t => t.BillId == billOfExchangeId).SequenceSort();

            return results;
        }

        /// <inheritdoc />
        [LogMethod]
        public async Task<IEnumerable<BillOfExchange>> GetIssuedByPartyId(int partyId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return Enumerable.Empty<BillOfExchange>();
            }

            var ds = await this.dataSourceProvider.BillsOfExchangeDataSource(cancellationToken);
            var results = ds.Data.Where(t => t.DrawerId == partyId);

            return results;
        }

        /// <inheritdoc />
        [LogMethod]
        public async Task<IEnumerable<BillOfExchange>> GetOwnedByPartyId(int partyId, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.CompletedTask;
                return Enumerable.Empty<BillOfExchange>();
            }

            var ds = await this.dataSourceProvider.BillsOfExchangeDataSource(cancellationToken);
            var dsEndorsments = await this.dataSourceProvider.EndorsmentsDataSource(cancellationToken);

            var ownedBillsOfExchange = ds.Data.Where(t => t.BeneficiaryId == partyId);

            var results = new List<BillOfExchange>();

            foreach (var billOfExchange in ownedBillsOfExchange)
            {
                if (!dsEndorsments.Data.Any(t => t.BillId == billOfExchange.Id) && !results.Any(t => t.Id == billOfExchange.Id))
                {
                    results.Add(billOfExchange);
                }
            }

            var endorsments = dsEndorsments.Data.Where(t => t.NewBeneficiaryId == partyId && !dsEndorsments.Data.Any(u => u.PreviousEndorsementId == t.Id && u.BillId == t.BillId))
                .Select(t => t.BillId).Distinct();

            var endorsmentsBillsOfExchanges = ds.Data.Where(t => endorsments.Contains(t.Id) && !results.Any(u => u.Id == t.Id));

            results.AddRange(endorsmentsBillsOfExchanges);

            return results;
        }
    }
}