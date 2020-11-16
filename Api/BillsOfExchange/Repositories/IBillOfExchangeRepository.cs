using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Models;

namespace BillsOfExchange.Repositories
{
    /// <summary>
    /// BillOfExchange repository
    /// </summary>
    public interface IBillOfExchangeRepository
    {
        /// <summary>
        /// Počet řádků v DataSource
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetRowsCount(CancellationToken cancellationToken);

        /// <summary>
        /// Stránkovaný seznam směnek
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Models.BillOfExchange>> Get(int page, int pageSize, CancellationToken cancellationToken);

        /// <summary>
        /// Směnka dle ID
        /// </summary>
        /// <param name="billOfExchangeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Models.BillOfExchange> GetById(int billOfExchangeId, CancellationToken cancellationToken);

        /// <summary>
        /// Rubopisy směnky
        /// </summary>
        /// <param name="billOfExchangeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Models.Endorsment>> GetEndorsments(int billOfExchangeId, CancellationToken cancellationToken);

        /// <summary>
        /// Vydané směnky osoby dle ID osoby
        /// </summary>
        /// <param name="partyId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BillOfExchange>> GetIssuedByPartyId(int partyId, CancellationToken cancellationToken);

        /// <summary>
        /// Vlastněné směnky osoby dle ID osoby
        /// </summary>
        /// <param name="partyId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BillOfExchange>> GetOwnedByPartyId(int partyId, CancellationToken cancellationToken);
    }
}