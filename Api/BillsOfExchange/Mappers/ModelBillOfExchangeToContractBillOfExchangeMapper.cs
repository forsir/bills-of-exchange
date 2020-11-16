using BillsOfExchange.Base;
using BillsOfExchange.Extensions;
using BillsOfExchange.Models;

namespace BillsOfExchange.Mappers
{
    /// <inheritdoc />
    public class ModelBillOfExchangeToContractBillOfExchangeMapper : IMapper<Models.BillOfExchange, Contracts.BillOfExchange>
    {
        private readonly IMapper<Models.Party, Contracts.Party> modelPartyToContractPartyMapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelPartyToContractPartyMapper"></param>
        public ModelBillOfExchangeToContractBillOfExchangeMapper(
            IMapper<Models.Party, Contracts.Party> modelPartyToContractPartyMapper
        )
        {
            this.modelPartyToContractPartyMapper = modelPartyToContractPartyMapper;
        }

        /// <inheritdoc />
        public void Map(BillOfExchange source, Contracts.BillOfExchange destination)
        {
            destination.Id = source.Id;
            destination.Amount = source.Amount;
            if (source.Beneficiary != null)
            {
                destination.Beneficiary = this.modelPartyToContractPartyMapper.Map(source.Beneficiary);
            }

            if (source.Drawer != null)
            {
                destination.Drawer = this.modelPartyToContractPartyMapper.Map(source.Drawer);
            }

            if (source.ValidatorResult == null)
            {
                source.ValidatorResult = new ValidatorResult();
            }

            destination.ValidatorResult = source.ValidatorResult;
        }
    }
}