using System;
using System.Collections.Generic;
using System.Linq;
using BillsOfExchange.BusinessLayer.Dto;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.BusinessLayer.Converters
{
	public class PartyConverter : IPartyConverter
	{
		private IPartyRepository PartyRepository { get; }

		public PartyConverter(IPartyRepository partyRepository)
		{
			PartyRepository = partyRepository;
		}

		public List<PartyListDto> GetList(int take, int skip)
		{
			IEnumerable<Party> list = PartyRepository.Get(take, skip);
			return list.Select(s => new PartyListDto(s.Id, s.Name)).ToList();
		}

		public PartyDetailDto GetParty(int partyId)
		{
			IEnumerable<Party> list = PartyRepository.GetByIds(new List<int> { partyId });
			return list.Select(p => new PartyDetailDto(p.Id, p.Name)).FirstOrDefault();
		}
	}
}
