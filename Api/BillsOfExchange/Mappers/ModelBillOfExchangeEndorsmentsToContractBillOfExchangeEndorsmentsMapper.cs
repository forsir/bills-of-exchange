using System;
using System.Linq;
using BillsOfExchange.Base;
using BillsOfExchange.Contracts;
using BillsOfExchange.Extensions;
using BillOfExchangeEndorsments = BillsOfExchange.Models.BillOfExchangeEndorsments;

namespace BillsOfExchange.Mappers
{
    /// <inheritdoc />
    public class ModelBillOfExchangeEndorsmentsToContractBillOfExchangeEndorsmentsMapper : IMapper<Models.BillOfExchangeEndorsments, Contracts.BillOfExchangeEndorsments>
    {
        private readonly IMapper<Models.BillOfExchange, Contracts.BillOfExchange> modelBillOfExchangeToContractBillOfExchangeMapper;
        private readonly IMapper<Models.Endorsment, Contracts.Endorsment> modelEndorsmentToContractEndorsmentMapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelBillOfExchangeToContractBillOfExchangeMapper"></param>
        /// <param name="modelEndorsmentToContractEndorsmentMapper"></param>
        public ModelBillOfExchangeEndorsmentsToContractBillOfExchangeEndorsmentsMapper(
            IMapper<Models.BillOfExchange, Contracts.BillOfExchange> modelBillOfExchangeToContractBillOfExchangeMapper,
            IMapper<Models.Endorsment, Contracts.Endorsment> modelEndorsmentToContractEndorsmentMapper
        )
        {
            this.modelBillOfExchangeToContractBillOfExchangeMapper = modelBillOfExchangeToContractBillOfExchangeMapper;
            this.modelEndorsmentToContractEndorsmentMapper = modelEndorsmentToContractEndorsmentMapper;
        }

        /// <inheritdoc />
        public void Map(BillOfExchangeEndorsments source, Contracts.BillOfExchangeEndorsments destination)
        {
            if (source.BillOfExchange != null)
            {
                destination.BillOfExchange = this.modelBillOfExchangeToContractBillOfExchangeMapper.Map(source.BillOfExchange);
            }

            if (source.Endorsments != null && source.Endorsments.Any())
            {
                destination.Endorsments = this.modelEndorsmentToContractEndorsmentMapper.MapList(source.Endorsments);
            }
            else
            {
                destination.Endorsments = Array.Empty<Endorsment>();
            }

            if (source.ValidatorResult == null)
            {
                source.ValidatorResult = new ValidatorResult();
            }

            destination.ValidatorResult = source.ValidatorResult;
        }
    }
}