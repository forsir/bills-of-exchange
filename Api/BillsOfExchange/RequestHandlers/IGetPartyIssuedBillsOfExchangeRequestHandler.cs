using BillsOfExchange.Base;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.RequestHandlers
{
    /// <summary>
    /// Směnky, které vydala osoba dle ID osoby
    /// </summary>
    public interface IGetPartyIssuedBillsOfExchangeRequestHandler : IAsyncRequestHandler<int, IActionResult>
    {
    }
}