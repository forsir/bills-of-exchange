using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillsOfExchange.BusinessLayer.Converters;
using BillsOfExchange.BusinessLayer.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BillsOfExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EndorsementController : Controller
    {
        private IEndorsementConverter EndorsementConverter { get; }

        public EndorsementController(IEndorsementConverter endorsementConverter)
        {
            EndorsementConverter = endorsementConverter ?? new EndorsementConverter(null);
        }

        // GET: Parties
        public ActionResult<IEnumerable<EndorsmentListDto>> Index(int billOfExchangeId)
        {
            return EndorsementConverter.GetByBillOfExhange(billOfExchangeId);
        }
    }
}
