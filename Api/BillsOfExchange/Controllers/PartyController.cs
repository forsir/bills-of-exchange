using System.Collections.Generic;
using System.Threading.Tasks;
using BillsOfExchange.DTO;
using BillsOfExchange.Repository;
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
		public ActionResult<IEnumerable<PartyListDTO>> Index(int? page)
		{
			return PartyConverter.GetList(pageSize, pageSize * (page ?? 0));
		}
	}
}