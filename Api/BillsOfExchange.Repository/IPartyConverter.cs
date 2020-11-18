using System.Collections.Generic;
using BillsOfExchange.DTO;

namespace BillsOfExchange.Repository
{
	public interface IPartyConverter
	{
		List<PartyListDTO> GetList(int take, int skip);
	}
}