using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillsOfExchange.DataProvider;
using BillsOfExchange.DTO;

namespace BillsOfExchange.Repository
{
	public class EndorsementConverter : IEndorsementConverter
	{
		private IEndorsementRepository EndorsementRepository { get; }

		public EndorsementConverter(IEndorsementRepository endorsementRepository = null)
		{
			EndorsementRepository = endorsementRepository ?? new EndorsementRepository();
		}

		public List<EndorsmentListDTO> GetByBillOfExhange(int billOfExhangeId)
		{
			IReadOnlyList<IEnumerable<DataProvider.Models.Endorsement>> list = EndorsementRepository.GetByBillIds(new List<int> { billOfExhangeId });

			return list.FirstOrDefault().Select(e => new EndorsmentListDTO(e.Id, "")).ToList() ?? new List<EndorsmentListDTO>();
		}
	}
}
