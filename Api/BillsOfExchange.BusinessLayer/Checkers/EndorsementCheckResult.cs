using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.BusinessLayer.Checkers
{
    public struct EndorsementCheckResult
    {
        public EndorsementCheckResult(bool result, string message = "", List<Endorsement> endorsements = null) :
           this(result, message, endorsements?.ToArray())
        {
        }

        public EndorsementCheckResult(bool result, string message, params Endorsement[] endorsements)
        {
            IsCorrect = result;
            Message = message;
            WrongEndorsements = endorsements ?? new Endorsement[] { };
        }

        public bool IsCorrect { get; }

        public string Message { get; }

        public Endorsement[] WrongEndorsements { get; }
    }
}
