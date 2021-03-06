﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfExchange.BusinessLayer.Dto
{
	public class EndorsmentListDto
	{
		public int Id { get; set; }

		/// <summary>
		/// New beneficiary entitled to the Bill
		/// </summary>
		public string NewBeneficiary { get; set; }
		public int NewBeneficiaryId { get; set; }

		public EndorsmentListDto(int id, int newBeneficiaryId, string newBeneficiary)
		{
			Id = id;
			NewBeneficiaryId = newBeneficiaryId;
			NewBeneficiary = newBeneficiary;
		}
	}
}
