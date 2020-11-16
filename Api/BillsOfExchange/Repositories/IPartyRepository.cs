using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Models;

namespace BillsOfExchange.Repositories
{
    /// <summary>
    /// Party repository
    /// </summary>
    public interface IPartyRepository
    {
        /// <summary>
        /// Počet řádků v DataSource
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetRowsCount(CancellationToken cancellationToken);

        /// <summary>
        /// Stránkovaný seznam osob
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Party>> Get(int page, int pageSize, CancellationToken cancellationToken);

        /// <summary>
        /// Osoba dle ID
        /// </summary>
        /// <param name="partyId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Party> GetById(int partyId, CancellationToken cancellationToken);
    }
}