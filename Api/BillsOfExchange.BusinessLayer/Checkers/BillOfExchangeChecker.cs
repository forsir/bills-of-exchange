using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.BusinessLayer.Checkers
{
    public class BillOfExchangeChecker : IBillOfExchangeChecker
    {
        public BillOfExchangeCheckResult BillOfExchangeCheck(BillOfExchange billOfExchange)
        {
            if (billOfExchange.BeneficiaryId == billOfExchange.DrawerId)
            {
                return new BillOfExchangeCheckResult(false, $"BillOfExchange with id {billOfExchange.Id} have same BeneficiaryId and DrawerId.");
            }
            return new BillOfExchangeCheckResult(true, String.Empty);
        }
    }
}
