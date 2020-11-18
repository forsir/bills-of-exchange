using System.Collections.Generic;
using BillsOfExchange.BusinessLayer.Converters;
using BillsOfExchange.BusinessLayer.Dto;
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
            BillsOfExchangeConverter = billsOfExchangeConverter ?? new BillOfExchangeConverter(null);
        }

        // GET: Parties
        public ActionResult<IEnumerable<BillOfExchangeListDto>> Index(int? page)
        {
            return BillsOfExchangeConverter.GetList(pageSize, pageSize * (page ?? 0));
        }

        public List<BillOfExchangeDetailDto> GetByBeneficiary(int beneficiaryId)
        {
            return BillsOfExchangeConverter.GetByBeneficiary(beneficiaryId);
        }

        public List<BillOfExchangeDetailDto> GetByDrawer(int drawerId)
        {
            return BillsOfExchangeConverter.GetByDrawer(drawerId);
        }
    }
}
