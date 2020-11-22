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

		public EndorsementConverter(IEndorsementRepository endorsementRepository = null, IEndorsementChecker endorsementChecker = null, IBillOfExchangeRepository billOfExchangeRepository = null)
		{
			EndorsementRepository = endorsementRepository ?? new EndorsementRepository();
			this.EndorsementChecker = endorsementChecker ?? new EndorsementChecker();
			this.BillOfExchangeRepository = billOfExchangeRepository ?? new BillOfExchangeRepository();
		}

		public List<EndorsmentListDto> GetByBillOfExhange(int billOfExhangeId)
		{
			IEnumerable<Endorsement> list = EndorsementRepository.GetByBillIds(new List<int> { billOfExhangeId }).FirstOrDefault()?.ToList() ?? new List<Endorsement>();
			BillOfExchange billOfExchange = BillOfExchangeRepository.GetByIds(new List<int> { billOfExhangeId }).First();

			EndorsementCheckResult result = EndorsementChecker.CheckList(billOfExchange, list);
			if (!result.IsCorrect)
			{
				throw new Exception(result.Message);
			}
			return list.Select(e => new EndorsmentListDto(e.Id, "")).ToList();
		}
	}
}
