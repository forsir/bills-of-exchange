using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Contracts;
using BillsOfExchange.RequestHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
    /// <summary>
    /// Směnky 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class BillsOfExchangeController : ControllerBase
    {
        private readonly Lazy<IGetBillsOfExchangeRequestHandler> getBillsOfExchangeRequestHandler;
        private readonly Lazy<IGetEndorsmentsRequestHandler> getEndorsmentsRequestHandler;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="getBillsOfExchangeRequestHandler"><see cref="T:BillsOfExchange.RequestHandlers.IGetBillsOfExchangeRequestHandler"/></param>
        /// <param name="getEndorsmentsRequestHandler"><see cref="T:BillsOfExchange.RequestHandlers.IGetEndorsmentsRequestHandler"/></param>
        public BillsOfExchangeController(
            Lazy<IGetBillsOfExchangeRequestHandler> getBillsOfExchangeRequestHandler,
            Lazy<IGetEndorsmentsRequestHandler> getEndorsmentsRequestHandler
        )
        {
            this.getBillsOfExchangeRequestHandler = getBillsOfExchangeRequestHandler;
            this.getEndorsmentsRequestHandler = getEndorsmentsRequestHandler;
        }

        /// <summary>
        /// Směnky - stránkovaně
        /// </summary>
        /// <param name="pageRequest"><see cref="T:BillsOfExchange.Contracts.PageRequest"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(BillsOfExchangeController) + nameof(Get))]
        [ProducesResponseType(typeof(Contracts.PagedResult<Contracts.BillOfExchange>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get([FromQuery] PageRequest pageRequest, CancellationToken cancellationToken)
        {
            return this.getBillsOfExchangeRequestHandler.Value.ExecuteAsync(pageRequest, cancellationToken);
        }

        /// <summary>
        /// Směnka s rubopisy dle ID směnky
        /// </summary>
        /// <param name="billOfExchangeId">ID směnky</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{billOfExchangeId}/Endorsments")]
        [ProducesResponseType(typeof(Contracts.BillOfExchangeEndorsments), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetEndorsments(int billOfExchangeId, CancellationToken cancellationToken)
        {
            return this.getEndorsmentsRequestHandler.Value.ExecuteAsync(billOfExchangeId, cancellationToken);
        }
    }
}