using BillsOfExchange.Base;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.RequestHandlers
{
    /// <summary>
    /// Směnky - stránkovaně
    /// </summary>
    public interface IGetBillsOfExchangeRequestHandler : IAsyncRequestHandler<Contracts.PageRequest, IActionResult>
    {
    }
}