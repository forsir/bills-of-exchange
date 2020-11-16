using BillsOfExchange.Base;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.RequestHandlers
{
    /// <summary>
    /// Směnky, které osoba vlastní dle ID osoby
    /// </summary>
    public interface IGetPartyOwnedBillsOfExchangeRequestHandler : IAsyncRequestHandler<int, IActionResult>
    {
    }
}