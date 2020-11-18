using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.BusinessLayer.Checkers
{
    public interface IBillOfExchangeChecker
    {
        BillOfExchangeCheckResult BillOfExchangeCheck(BillOfExchange billOfExchange);
    }
}