using System.Collections.Generic;
using BillsOfExchange.BusinessLayer.Dto;

namespace BillsOfExchange.BusinessLayer.Converters
{
	public interface IBillsOfExchangeConverter
	{
		List<BillOfExchangeDetailDto> GetByBeneficiary(int beneficiaryId);
		List<BillOfExchangeDetailDto> GetByDrawer(int drawerId);
		List<BillOfExchangeListDto> GetList(int take, int skip);
	}
}