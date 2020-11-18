using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillsOfExchange.DTO;
using BillsOfExchange.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EndorsementController : Controller
	{
		private IEndorsementConverter EndorsementConverter { get; }
		private const int pageSize = 10;

		public EndorsementController(IEndorsementConverter endorsementConverter)
		{
			EndorsementConverter = endorsementConverter ?? new EndorsementConverter(null);
		}

		// GET: Parties
		public ActionResult<IEnumerable<EndorsmentListDTO>> Index(int? page)
		{
			return EndorsementConverter.GetList(pageSize, pageSize * (page ?? 0));
		}
	}
}
