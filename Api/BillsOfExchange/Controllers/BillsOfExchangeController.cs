﻿using System.Collections.Generic;
using BillsOfExchange.DTO;
using BillsOfExchange.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BillsOfExchangeController : ControllerBase
	{
		private IBillsOfExchangeConverter BillsOfExchangeConverter { get; }
		private const int pageSize = 10;

		public BillsOfExchangeController(IBillsOfExchangeConverter billsOfExchangeConverter)
		{
			BillsOfExchangeConverter = billsOfExchangeConverter ?? new BillsOfExchangeConverter(null);
		}

		// GET: Parties
		public ActionResult<IEnumerable<BillsOfExchangeListDTO>> Index(int? page)
		{
			return BillsOfExchangeConverter.GetList(pageSize, pageSize * (page ?? 0));
		}

		public List<BillsOfExchangeByBeneficiaryListDTO> GetByBeneficiary(int beneficiaryId)
		{
			return BillsOfExchangeConverter.GetByBeneficiary(beneficiaryId);
		}

		public List<BillsOfExchangeByDrawerListDTO> GetByDrawer(int drawerId)
		{
			return BillsOfExchangeConverter.GetByDrawer(drawerId);
		}
	}
}
