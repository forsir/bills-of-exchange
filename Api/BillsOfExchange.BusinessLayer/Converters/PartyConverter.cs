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
    }
}
