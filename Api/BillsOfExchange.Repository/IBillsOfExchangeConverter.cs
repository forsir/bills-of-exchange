using System.Collections.Generic;
using BillsOfExchange.DTO;

namespace BillsOfExchange.Repository
{
	public interface IBillsOfExchangeConverter
	{
		List<BillsOfExchangeByBeneficiaryListDTO> GetByBeneficiary(int beneficiaryId);
		List<BillsOfExchangeByDrawerListDTO> GetByDrawer(int drawerId);
		List<BillsOfExchangeListDTO> GetList(int skip, int take);
	}
}