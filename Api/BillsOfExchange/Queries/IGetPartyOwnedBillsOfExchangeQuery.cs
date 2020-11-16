using BillsOfExchange.Base;

namespace BillsOfExchange.Queries
{
    /// <summary>
    /// Směnky, které osoba vlastní dle ID osoby
    /// </summary>
    public interface IGetPartyOwnedBillsOfExchangeQuery : IAsyncQuery<int, Models.PartyBillsOfExchange>
    {
    }
}