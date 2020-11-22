using System.Collections.Generic;
using System.Threading.Tasks;
using BillsOfExchange.BusinessLayer.Converters;
using BillsOfExchange.BusinessLayer.Dto;
using BillsOfExchange.DataProvider;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
	[ApiController]
	public class PartyController : ControllerBase
	{
		private IPartyConverter PartyConverter { get; }
		private const int pageSize = 10;

		public PartyController(IPartyConverter partyConverter = null)
		{
			PartyConverter = partyConverter ?? new PartyConverter(new PartyRepository());
		}

		// GET: Parties
		[Route("parties/{page:int?}")]
		public ActionResult<IEnumerable<PartyListDto>> Index(int? page)
		{
			return PartyConverter.GetList(pageSize, pageSize * (page ?? 0));
		}

		[Route("party/{partyId:int}")]
		public ActionResult<PartyDetailDto> GetParty(int partyId)
		{
			return PartyConverter.GetParty(partyId);
		}
	}
}