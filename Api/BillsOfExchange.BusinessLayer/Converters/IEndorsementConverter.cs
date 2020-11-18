using System.Collections.Generic;
using BillsOfExchange.BusinessLayer.Dto;
using BillsOfExchange.DataProvider;

namespace BillsOfExchange.BusinessLayer.Converters
{
    public interface IEndorsementConverter
    {
        List<EndorsmentListDto> GetByBillOfExhange(int billOfExhangeId);
    }
}