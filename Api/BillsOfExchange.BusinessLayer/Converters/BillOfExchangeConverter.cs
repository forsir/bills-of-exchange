using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillsOfExchange.BusinessLayer.Checkers;
using BillsOfExchange.BusinessLayer.Dto;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.BusinessLayer.Converters
{
	public class BillOfExchangeConverter : IBillsOfExchangeConverter
	{
		private IBillOfExchangeRepository BillOfExchangeRepository { get; }

		private IPartyRepository PartyRepository { get; }

		private IBillOfExchangeChecker BillOfExchangeChecker { get; }

		private IEndorsementRepository EndorsementRepository { get; }

		public BillOfExchangeConverter(
			IBillOfExchangeRepository billOfExchangeRepository = null,
			IPartyRepository partyRepository = null,
			IBillOfExchangeChecker billOfExchangeChecker = null,
			IEndorsementRepository endorsementRepository = null)
		{
			BillOfExchangeRepository = billOfExchangeRepository ?? new BillOfExchangeRepository();
			PartyRepository = partyRepository ?? new PartyRepository();
			BillOfExchangeChecker = billOfExchangeChecker ?? new BillOfExchangeChecker();
			EndorsementRepository = endorsementRepository ?? new EndorsementRepository();
		}

		private Dictionary<int, string> GetPartiesDictionary(List<int> ids)
		{
			IReadOnlyList<Party> result = PartyRepository.GetByIds(ids).ToList();
			return result.ToDictionary(p => p.Id, p => p.Name);
		}

		private List<int> GetPartiesIds(List<BillOfExchange> billOfExchanges)
		{
			return billOfExchanges.Select(b => b.BeneficiaryId).Concat(billOfExchanges.Select(b => b.DrawerId)).Distinct().OrderBy(d => d).ToList();
		}

		public List<BillOfExchangeListDto> GetList(int take, int skip)
		{
			var list = BillOfExchangeRepository.Get(take, skip).ToList();
			Dictionary<int, string> names = GetPartiesDictionary(GetPartiesIds(list));
			return list.ConvertAll(b => new BillOfExchangeListDto(b.Id, b.DrawerId, names[b.DrawerId], b.BeneficiaryId, names[b.BeneficiaryId], b.Amount));
		}

		public BillOfExchangeDetailDto GetBillOfExchange(int billId)
		{
			BillOfExchange bill = BillOfExchangeRepository.GetByIds(new List<int> { billId }).First();

			BillOfExchangeCheckResult result = BillOfExchangeChecker.BillOfExchangeCheck(bill);
			if (!result.IsCorrect)
			{
				throw new Exception(result.Message);
			}

			Dictionary<int, string> names = GetPartiesDictionary(GetPartiesIds(new List<BillOfExchange> { bill }));
			return new BillOfExchangeDetailDto(bill.Id, bill.DrawerId, names[bill.DrawerId], bill.BeneficiaryId, names[bill.BeneficiaryId], bill.Amount);
		}

		public List<BillOfExchangeDetailDto> GetByBeneficiary(int beneficiaryId)
		{
			List<BillOfExchange> allBillsByBeneficiary = BillOfExchangeRepository.GetByBeneficiaryIds(new List<int> { beneficiaryId }).FirstOrDefault()?.ToList() ?? new List<BillOfExchange>();
			var billIdsWithAtLeastOneEndorsement = new HashSet<int>(EndorsementRepository.GetByBillIds(allBillsByBeneficiary.ConvertAll(b => b.Id)).Select(e => e.First().BillId));
			var billsByBeneficiaryWithoutAtLeastOneEndorsement = allBillsByBeneficiary.Where(b => !billIdsWithAtLeastOneEndorsement.Contains(b.Id)).ToList();

			var endorsementsWithBenficiaryList = EndorsementRepository.GetByNewBeneficiaryIds(new List<int> { beneficiaryId }).FirstOrDefault()?.ToList();
			IReadOnlyList<IEnumerable<Endorsement>> allEndorsementByBillList = EndorsementRepository.GetByBillIds(endorsementsWithBenficiaryList.Select(e => e.BillId).Distinct().ToList());
			var lastEndorsementWithBeneficiaryList = allEndorsementByBillList.Select(el => el.Last()).Where(el => el.NewBeneficiaryId == beneficiaryId).ToList();
			var billsByLastEndorsement = BillOfExchangeRepository.GetByIds(lastEndorsementWithBeneficiaryList.Select(e => e.BillId).ToList()).ToList();
			var finishedList = billsByBeneficiaryWithoutAtLeastOneEndorsement.Concat(billsByLastEndorsement).Distinct().ToList();

			Dictionary<int, string> names = GetPartiesDictionary(GetPartiesIds(finishedList));

			return finishedList.ConvertAll(b => new BillOfExchangeDetailDto(b.Id, b.DrawerId, names[b.DrawerId], b.BeneficiaryId, names[b.BeneficiaryId], b.Amount));
		}

		public List<BillOfExchangeDetailDto> GetByDrawer(int drawerId)
		{
			List<BillOfExchange> list = BillOfExchangeRepository.GetByDrawerIds(new List<int> { drawerId }).FirstOrDefault()?.ToList() ?? new List<BillOfExchange>();
			Dictionary<int, string> names = GetPartiesDictionary(GetPartiesIds(list));
			return list.ConvertAll(b => new BillOfExchangeDetailDto(b.Id, b.DrawerId, names[b.DrawerId], b.BeneficiaryId, names[b.BeneficiaryId], b.Amount));
		}
	}
}
