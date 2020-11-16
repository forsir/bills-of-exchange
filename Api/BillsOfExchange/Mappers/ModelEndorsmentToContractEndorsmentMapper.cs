using BillsOfExchange.Base;
using BillsOfExchange.Extensions;
using BillsOfExchange.Models;

namespace BillsOfExchange.Mappers
{
    /// <inheritdoc />
    public class ModelEndorsmentToContractEndorsmentMapper : IMapper<Models.Endorsment, Contracts.Endorsment>
    {
        private readonly IMapper<Models.Party, Contracts.Party> modelPartyToContractPartyMapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelPartyToContractPartyMapper"></param>
        public ModelEndorsmentToContractEndorsmentMapper(
            IMapper<Models.Party, Contracts.Party> modelPartyToContractPartyMapper
        )
        {
            this.modelPartyToContractPartyMapper = modelPartyToContractPartyMapper;
        }

        /// <inheritdoc />
        public void Map(Endorsment source, Contracts.Endorsment destination)
        {
            destination.Id = source.Id;
            destination.BillId = source.BillId;
            destination.NewBeneficiaryId = source.NewBeneficiaryId;

            if (source.NewBeneficiary != null)
            {
                destination.NewBeneficiary = this.modelPartyToContractPartyMapper.Map(source.NewBeneficiary);
            }

            destination.PreviousEndorsementId = source.PreviousEndorsementId;
        }
    }
}