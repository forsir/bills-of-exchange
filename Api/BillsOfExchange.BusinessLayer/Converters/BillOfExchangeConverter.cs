using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillsOfExchange.BusinessLayer.Dto;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.BusinessLayer.Converters
{
	public class BillOfExchangeConverter : IBillsOfExchangeConverter
	{
		private IBillOfExchangeRepository BillOfExchangeRepository { get; }

		private IPartyRepository PartyRepository { get; }

		public BillOfExchangeConverter(IBillOfExchangeRepository billOfExchangeRepository = null, IPartyRepository partyRepository = null)
		{
			BillOfExchangeRepository = billOfExchangeRepository ?? new BillOfExchangeRepository();
			PartyRepository = partyRepository ?? new PartyRepository();
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
			Dictionary<int, string> names = GetPartiesDictionary(GetPartiesIds(new List<BillOfExchange> { bill }));
			return new BillOfExchangeDetailDto(bill.Id, bill.DrawerId, names[bill.DrawerId], bill.BeneficiaryId, names[bill.BeneficiaryId], bill.Amount);
		}

		public List<BillOfExchangeDetailDto> GetByBeneficiary(int beneficiaryId)
		{
			List<BillOfExchange> list = BillOfExchangeRepository.GetByBeneficiaryIds(new List<int> { beneficiaryId }).FirstOrDefault()?.ToList() ?? new List<BillOfExchange>();
			Dictionary<int, string> names = GetPartiesDictionary(GetPartiesIds(list));
			return list.ConvertAll(b => new BillOfExchangeDetailDto(b.Id, b.DrawerId, names[b.DrawerId], b.BeneficiaryId, names[b.BeneficiaryId], b.Amount));
		}

		public List<BillOfExchangeDetailDto> GetByDrawer(int drawerId)
		{
			List<BillOfExchange> list = BillOfExchangeRepository.GetByDrawerIds(new List<int> { drawerId }).FirstOrDefault()?.ToList() ?? new List<BillOfExchange>();
			Dictionary<int, string> names = GetPartiesDictionary(GetPartiesIds(list));
			return list.ConvertAll(b => new BillOfExchangeDetailDto(b.Id, b.DrawerId, names[b.DrawerId], b.BeneficiaryId, names[b.BeneficiaryId], b.Amount));
		}
	}
}
