using BillsOfExchange.Base;
using BillsOfExchange.Models;

namespace BillsOfExchange.Queries
{
    /// <summary>
    /// Směnky - stránkovaně
    /// </summary>
    public interface IGetBillsOfExchangeQuery : IAsyncQuery<int, int, PagedResult<Models.BillOfExchange>>
    {
    }
}