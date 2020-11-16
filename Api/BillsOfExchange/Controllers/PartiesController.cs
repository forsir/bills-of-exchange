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
    /// Osoby
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class PartiesController : ControllerBase
    {
        private readonly Lazy<IGetPartiesRequestHandler> getPartiesRequestHandler;
        private readonly Lazy<IGetPartyIssuedBillsOfExchangeRequestHandler> getPartyIssuedBillsOfExchangeRequestHandler;
        private readonly Lazy<IGetPartyOwnedBillsOfExchangeRequestHandler> getPartyOwnedBillsOfExchangeRequestHandler;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="getPartiesRequestHandler"><see cref="T:BillsOfExchange.RequestHandlers.IGetPartiesRequestHandler"/></param>
        /// <param name="getPartyIssuedBillsOfExchangeRequestHandler"><see cref="T:BillsOfExchange.RequestHandlers.IGetPartyIssuedBillsOfExchangeRequestHandler"/></param>
        /// <param name="getPartyOwnedBillsOfExchangeRequestHandler"><see cref="T:BillsOfExchange.RequestHandlers.IGetPartyOwnedBillsOfExchangeRequestHandler"/></param>
        public PartiesController(
            Lazy<IGetPartiesRequestHandler> getPartiesRequestHandler,
            Lazy<IGetPartyIssuedBillsOfExchangeRequestHandler> getPartyIssuedBillsOfExchangeRequestHandler,
            Lazy<IGetPartyOwnedBillsOfExchangeRequestHandler> getPartyOwnedBillsOfExchangeRequestHandler
        )
        {
            this.getPartiesRequestHandler = getPartiesRequestHandler;
            this.getPartyIssuedBillsOfExchangeRequestHandler = getPartyIssuedBillsOfExchangeRequestHandler;
            this.getPartyOwnedBillsOfExchangeRequestHandler = getPartyOwnedBillsOfExchangeRequestHandler;
        }

        /// <summary>
        /// Osoby - stránkovaně
        /// </summary>
        /// <param name="pageRequest"><see cref="T:BillsOfExchange.Contracts.PageRequest"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(PartiesController) + nameof(Get))]
        [ProducesResponseType(typeof(Contracts.PagedResult<Contracts.Party>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get([FromQuery] PageRequest pageRequest, CancellationToken cancellationToken)
        {
            return this.getPartiesRequestHandler.Value.ExecuteAsync(pageRequest, cancellationToken);
        }

        /// <summary>
        /// Směnky, které vydala osoba dle ID osoby
        /// </summary>
        /// <param name="partyId">ID osoby</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{partyId}/BillsOfExchange/Issued")]
        [ProducesResponseType(typeof(Contracts.PartyBillsOfExchange), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetPartyIssuedBillsOfExchange(int partyId, CancellationToken cancellationToken)
        {
            return this.getPartyIssuedBillsOfExchangeRequestHandler.Value.ExecuteAsync(partyId, cancellationToken);
        }

        /// <summary>
        /// Směnky, které osoba vlastní dle ID osoby
        /// </summary>
        /// <param name="partyId">ID osoby</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{partyId}/BillsOfExchange/Owned")]
        [ProducesResponseType(typeof(Contracts.PartyBillsOfExchange), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetPartyOwnedBillsOfExchange(int partyId, CancellationToken cancellationToken)
        {
            return this.getPartyOwnedBillsOfExchangeRequestHandler.Value.ExecuteAsync(partyId, cancellationToken);
        }
    }
}