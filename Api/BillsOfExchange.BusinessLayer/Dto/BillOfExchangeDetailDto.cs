using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfExchange.BusinessLayer.Dto
{
    public class BillOfExchangeDetailDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Party that issued the Bill
        /// </summary>
        public int DrawerId { get; set; }

        /// <summary>
        /// Party that was the Bill issued to
        /// </summary>
        public int BeneficiaryId { get; set; }

        /// <summary>
        /// Amount of goods the Bill represents
        /// </summary>
        public decimal Amount { get; set; }

        public BillOfExchangeDetailDto(int id, int drawerId, int beneficiaryId, decimal amount)
        {
            Id = id;
            DrawerId = drawerId;
            BeneficiaryId = beneficiaryId;
            Amount = amount;
        }
    }
}
