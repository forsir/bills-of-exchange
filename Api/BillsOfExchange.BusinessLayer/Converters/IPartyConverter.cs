using System.Collections.Generic;
using BillsOfExchange.BusinessLayer.Dto;

namespace BillsOfExchange.BusinessLayer.Converters
{
	public interface IPartyConverter
	{
		List<PartyListDto> GetList(int take, int skip);

		PartyDetailDto GetParty(int partyId);
	}
}