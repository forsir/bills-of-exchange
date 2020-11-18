using System.Collections.Generic;
using System.Threading.Tasks;
using BillsOfExchange.BusinessLayer.Converters;
using BillsOfExchange.BusinessLayer.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartyController : ControllerBase
    {
        private IPartyConverter PartyConverter { get; }
        private const int pageSize = 10;

        public PartyController(IPartyConverter partyConverter)
        {
            PartyConverter = partyConverter ?? new PartyConverter(null);
        }

        // GET: Parties
        public ActionResult<IEnumerable<PartyListDto>> Index(int? page)
        {
            return PartyConverter.GetList(pageSize, pageSize * (page ?? 0));
        }
    }
}