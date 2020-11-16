using System;
using System.Linq;
using BillsOfExchange.Base;
using BillsOfExchange.Extensions;
using BillsOfExchange.Models;

namespace BillsOfExchange.Mappers
{
    /// <inheritdoc />
    public class ModelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper : IMapper<Models.PartyBillsOfExchange, Contracts.PartyBillsOfExchange>
    {
        private readonly IMapper<Models.BillOfExchange, Contracts.BillOfExchange> modelBillOfExchangeToContractBillOfExchangeMapper;
        private readonly IMapper<Models.Party, Contracts.Party> modelPartyToContractPartyMapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelBillOfExchangeToContractBillOfExchangeMapper"></param>
        /// <param name="modelPartyToContractPartyMapper"></param>
        public ModelPartyBillsOfExchangeToContractPartyBillsOfExchangeMapper(
            IMapper<Models.BillOfExchange, Contracts.BillOfExchange> modelBillOfExchangeToContractBillOfExchangeMapper,
            IMapper<Models.Party, Contracts.Party> modelPartyToContractPartyMapper
        )
        {
            this.modelBillOfExchangeToContractBillOfExchangeMapper = modelBillOfExchangeToContractBillOfExchangeMapper;
            this.modelPartyToContractPartyMapper = modelPartyToContractPartyMapper;
        }

        /// <inheritdoc />
        public void Map(PartyBillsOfExchange source, Contracts.PartyBillsOfExchange destination)
        {
            if (source.Party != null)
            {
                destination.Party = this.modelPartyToContractPartyMapper.Map(source.Party);
            }

            if (source.BillsOfExchange != null && source.BillsOfExchange.Any())
            {
                destination.BillsOfExchange = this.modelBillOfExchangeToContractBillOfExchangeMapper.MapList(source.BillsOfExchange);
            }
            else
            {
                destination.BillsOfExchange = Array.Empty<Contracts.BillOfExchange>();
            }

            if (source.ValidatorResult == null)
            {
                source.ValidatorResult = new ValidatorResult();
            }

            destination.ValidatorResult = source.ValidatorResult;
        }
    }
}