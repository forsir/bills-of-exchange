using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfExchange.DTO
{
	public class EndorsmentListDTO
	{
		public int Id { get; set; }

		/// <summary>
		/// New beneficiary entitled to the Bill
		/// </summary>
		public string NewBeneficiary { get; set; }

		public EndorsmentListDTO(int id, string newBeneficiary)
		{
			Id = id;
			NewBeneficiary = newBeneficiary;
		}
	}
}
