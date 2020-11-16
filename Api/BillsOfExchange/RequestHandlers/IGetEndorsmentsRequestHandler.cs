using BillsOfExchange.Base;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.RequestHandlers
{
    /// <summary>
    /// Směnka s rubopisy dle ID směnky
    /// </summary>
    public interface IGetEndorsmentsRequestHandler : IAsyncRequestHandler<int, IActionResult>
    {
    }
}