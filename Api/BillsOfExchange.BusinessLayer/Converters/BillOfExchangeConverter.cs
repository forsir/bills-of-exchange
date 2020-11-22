using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillsOfExchange.BusinessLayer.Dto;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.BusinessLayer.Converters
{
	public class BillOfExchangeConverter : IBillsOfExchangeConverter
	{
		private IBillOfExchangeRepository BillOfExchangeRepository { get; }

		public BillOfExchangeConverter(IBillOfExchangeRepository billOfExchangeRepository)
		{
			BillOfExchangeRepository = billOfExchangeRepository ?? new BillOfExchangeRepository();
		}

		public List<BillOfExchangeListDto> GetList(int take, int skip)
		{
			var list = BillOfExchangeRepository.Get(take, skip).ToList();
			return list.ConvertAll(b => new BillOfExchangeListDto(b.Id, b.DrawerId, b.BeneficiaryId, b.Amount));
		}

		public List<BillOfExchangeDetailDto> GetByBeneficiary(int beneficiaryId)
		{
			List<BillOfExchange> list = BillOfExchangeRepository.GetByBeneficiaryIds(new List<int> { beneficiaryId }).FirstOrDefault()?.ToList() ?? new List<BillOfExchange>();
			return list.ConvertAll(b => new BillOfExchangeDetailDto(b.Id, b.DrawerId, b.BeneficiaryId, b.Amount));
		}

		public List<BillOfExchangeDetailDto> GetByDrawer(int drawerId)
		{
			List<BillOfExchange> list = BillOfExchangeRepository.GetByDrawerIds(new List<int> { drawerId }).FirstOrDefault()?.ToList() ?? new List<BillOfExchange>();
			return list.ConvertAll(b => new BillOfExchangeDetailDto(b.Id, b.DrawerId, b.BeneficiaryId, b.Amount));
		}
	}
}
