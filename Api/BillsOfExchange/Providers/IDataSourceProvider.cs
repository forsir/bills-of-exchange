using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Models;

namespace BillsOfExchange.Providers
{
    /// <summary>
    /// DataSource Provider - data z json souborů
    /// </summary>
    public interface IDataSourceProvider
    {
        /// <summary>
        /// Data Endorsment
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DataSource<Models.Endorsment>> EndorsmentsDataSource(CancellationToken cancellationToken);

        /// <summary>
        /// Data BillOfExchange
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DataSource<Models.BillOfExchange>> BillsOfExchangeDataSource(CancellationToken cancellationToken);

        /// <summary>
        /// Data Party
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DataSource<Models.Party>> PartiesDataSource(CancellationToken cancellationToken);
    }
}