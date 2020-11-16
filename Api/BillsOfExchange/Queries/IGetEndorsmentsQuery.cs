using BillsOfExchange.Base;

namespace BillsOfExchange.Queries
{
    /// <summary>
    /// Směnka s rubopisy dle ID směnky
    /// </summary>
    public interface IGetEndorsmentsQuery : IAsyncQuery<int, Models.BillOfExchangeEndorsments>
    {
    }
}