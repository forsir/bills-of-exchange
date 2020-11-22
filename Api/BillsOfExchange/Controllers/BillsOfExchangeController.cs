using System.Collections.Generic;
using BillsOfExchange.BusinessLayer.Converters;
using BillsOfExchange.BusinessLayer.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
	[ApiController]
	public class BillsOfExchangeController : ControllerBase
	{
		private IBillsOfExchangeConverter BillsOfExchangeConverter { get; }
		private const int pageSize = 10;

		public BillsOfExchangeController(IBillsOfExchangeConverter billsOfExchangeConverter = null)
		{
			BillsOfExchangeConverter = billsOfExchangeConverter ?? new BillOfExchangeConverter(null);
		}

		[Route("bills/{page:int?}")]
		public ActionResult<IEnumerable<BillOfExchangeListDto>> Index(int? page)
		{
			return BillsOfExchangeConverter.GetList(pageSize, pageSize * (page ?? 0));
		}

		[Route("bill/{bullId:int}")]
		public ActionResult<BillOfExchangeDetailDto> GetBillOfExchange(int bullId)
		{
			return BillsOfExchangeConverter.GetBillOfExchange(bullId);
		}

		[Route("bills/bybeneficiary/{beneficiaryId:int}")]
		public List<BillOfExchangeDetailDto> GetByBeneficiary(int beneficiaryId)
		{
			return BillsOfExchangeConverter.GetByBeneficiary(beneficiaryId);
		}

		[Route("bills/bydrawer/{drawerId:int}")]
		public List<BillOfExchangeDetailDto> GetByDrawer(int drawerId)
		{
			return BillsOfExchangeConverter.GetByDrawer(drawerId);
		}
	}
}
