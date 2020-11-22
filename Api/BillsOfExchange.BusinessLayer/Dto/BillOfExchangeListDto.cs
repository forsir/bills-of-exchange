using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfExchange.BusinessLayer.Dto
{
	public class BillOfExchangeListDto
	{
		public int Id { get; set; }

		/// <summary>
		/// Party that issued the Bill
		/// </summary>
		public int DrawerId { get; set; }
		public string Drawer { get; set; }

		/// <summary>
		/// Party that was the Bill issued to
		/// </summary>
		public int BeneficiaryId { get; set; }
		public string Beneficiary { get; set; }

		/// <summary>
		/// Amount of goods the Bill represents
		/// </summary>
		public decimal Amount { get; set; }

		public BillOfExchangeListDto(int id, int drawerId, string drawer, int beneficiaryId, string beneficiary, decimal amount)
		{
			Id = id;
			DrawerId = drawerId;
			BeneficiaryId = beneficiaryId;
			Amount = amount;
			Drawer = drawer;
			Beneficiary = beneficiary;
		}
	}
}
