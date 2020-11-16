using BillsOfExchange.Base;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.RequestHandlers
{
    /// <summary>
    /// Osoby - stránkovaně
    /// </summary>
    public interface IGetPartiesRequestHandler : IAsyncRequestHandler<Contracts.PageRequest, IActionResult>
    {
    }
}