using BillsOfExchange.Base;
using BillsOfExchange.Models;

namespace BillsOfExchange.Queries
{
    /// <summary>
    /// Osoby - stránkovaně
    /// </summary>
    public interface IGetPartiesQuery : IAsyncQuery<int, int, PagedResult<Models.Party>>
    {
    }
}