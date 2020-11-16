using BillsOfExchange.Base;

namespace BillsOfExchange.Queries
{
    /// <summary>
    /// Směnky, které vydala osoba dle ID osoby
    /// </summary>
    public interface IGetPartyIssuedBillsOfExchangeQuery : IAsyncQuery<int, Models.PartyBillsOfExchange>
    {
    }
}