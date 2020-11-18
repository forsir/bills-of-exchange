using System;
using System.Collections.Generic;
using System.Text;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;
using BillsOfExchange.DTO;

namespace BillsOfExchange.Repository
{
	public class BillsOfExchangeConverter : IBillsOfExchangeConverter
	{
		private IBillOfExchangeRepository BillOfExchangeRepository { get; }

		public BillsOfExchangeConverter(IBillOfExchangeRepository billOfExchangeRepository)
		{
			BillOfExchangeRepository = billOfExchangeRepository ?? new BillOfExchangeRepository();
		}

		public List<BillsOfExchangeListDTO> GetList(int skip, int take)
		{
			return new List<BillsOfExchangeListDTO> { new BillsOfExchangeListDTO() };
		}

		public List<BillsOfExchangeByBeneficiaryListDTO> GetByBeneficiary(int beneficiaryId)
		{
			return new List<BillsOfExchangeByBeneficiaryListDTO> { new BillsOfExchangeByBeneficiaryListDTO() };
		}

		public List<BillsOfExchangeByDrawerListDTO> GetByDrawer(int drawerId)
		{
			return new List<BillsOfExchangeByDrawerListDTO> { new BillsOfExchangeByDrawerListDTO() };
		}
	}
}
