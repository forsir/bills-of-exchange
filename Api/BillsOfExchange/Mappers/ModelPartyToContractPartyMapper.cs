using BillsOfExchange.Base;
using BillsOfExchange.Models;

namespace BillsOfExchange.Mappers
{
    /// <inheritdoc />
    public class ModelPartyToContractPartyMapper : IMapper<Models.Party, Contracts.Party>
    {
        /// <inheritdoc />
        public void Map(Party source, Contracts.Party destination)
        {
            destination.Id = source.Id;
            destination.Name = source.Name;
            if (source.ValidatorResult == null)
            {
                source.ValidatorResult = new ValidatorResult();
            }

            destination.ValidatorResult = source.ValidatorResult;
        }
    }
}