using System;
using System.Collections.Generic;
using System.Linq;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;
using BillsOfExchange.DTO;

namespace BillsOfExchange.Repository
{
	public class PartyConverter : IPartyConverter
	{
		private IPartyRepository PartyRepository { get; set; }

		public PartyConverter(IPartyRepository partyRepository)
		{
			PartyRepository = partyRepository;
		}

		public List<PartyListDTO> GetList(int take, int skip)
		{
			IEnumerable<Party> list = PartyRepository.Get(take, skip);
			return list.Select(s => new PartyListDTO(s.Id, s.Name)).ToList();
		}
	}
}
