using System.Collections.Generic;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DTO;

namespace BillsOfExchange.Repository
{
	public interface IEndorsementConverter
	{
		List<EndorsmentListDTO> GetByBillOfExhange(int billOfExhangeId);
	}
}