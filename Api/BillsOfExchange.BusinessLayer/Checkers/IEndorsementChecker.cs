using System.Collections.Generic;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.BusinessLayer.Checkers
{
    public interface IEndorsementChecker
    {
        EndorsementCheckResult CheckList(BillOfExchange billOfExchange, IEnumerable<Endorsement> endorsemetList);
    }
}