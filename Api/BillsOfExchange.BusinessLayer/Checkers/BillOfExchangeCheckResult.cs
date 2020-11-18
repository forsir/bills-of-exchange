using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfExchange.BusinessLayer.Checkers
{
    public struct BillOfExchangeCheckResult
    {
        public bool IsCorrect { get; }

        public string Message { get; }

        public BillOfExchangeCheckResult(bool isCorrect, string message)
        {
            IsCorrect = isCorrect;
            Message = message;
        }

    }
}
