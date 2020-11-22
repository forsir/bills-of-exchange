using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BillsOfExchange.BusinessLayer.Converters;
using BillsOfExchange.BusinessLayer.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
	[ApiController]
	public class EndorsementController : Controller
	{
		private IEndorsementConverter EndorsementConverter { get; }

		public EndorsementController(IEndorsementConverter endorsementConverter = null)
		{
			EndorsementConverter = endorsementConverter ?? new EndorsementConverter(null);
		}

		[Route("endorsement/bybill/{billOfExchangeId:int}")]
		public ActionResult<IEnumerable<EndorsmentListDto>> Index(int billOfExchangeId)
		{
			try
			{
				return EndorsementConverter.GetByBillOfExhange(billOfExchangeId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
