using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillsOfExchange.BusinessLayer.Checkers;
using BillsOfExchange.BusinessLayer.Dto;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.BusinessLayer.Converters
{
	public class EndorsementConverter : IEndorsementConverter
	{
		private IEndorsementChecker EndorsementChecker { get; }

		private IEndorsementRepository EndorsementRepository { get; }

		private IBillOfExchangeRepository BillOfExchangeRepository { get; }

		private IPartyRepository PartyRepository { get; }

		public EndorsementConverter(IEndorsementRepository endorsementRepository = null, IEndorsementChecker endorsementChecker = null, IBillOfExchangeRepository billOfExchangeRepository = null, IPartyRepository partyRepository = null)
		{
			EndorsementRepository = endorsementRepository ?? new EndorsementRepository();
			this.EndorsementChecker = endorsementChecker ?? new EndorsementChecker();
			this.BillOfExchangeRepository = billOfExchangeRepository ?? new BillOfExchangeRepository();
			PartyRepository = partyRepository;
		}

		public List<EndorsmentListDto> GetByBillOfExhange(int billOfExhangeId)
		{
			IEnumerable<Endorsement> list = EndorsementRepository.GetByBillIds(new List<int> { billOfExhangeId }).FirstOrDefault()?.ToList() ?? new List<Endorsement>();
			BillOfExchange billOfExchange = BillOfExchangeRepository.GetByIds(new List<int> { billOfExhangeId }).First();

			var partyNamesDictionary = PartyRepository.GetByIds(list.Select(l => l.NewBeneficiaryId).ToList()).ToDictionary(p => p.Id, p => p.Name);

			EndorsementCheckResult result = EndorsementChecker.CheckList(billOfExchange, list);
			if (!result.IsCorrect)
			{
				throw new Exception(result.Message);
			}
			return list.Select(e => new EndorsmentListDto(e.Id, e.NewBeneficiaryId, partyNamesDictionary[e.NewBeneficiaryId])).ToList();
		}
	}
}
