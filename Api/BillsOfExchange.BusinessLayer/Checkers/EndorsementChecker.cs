using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BillsOfExchange.DataProvider.Models;

namespace BillsOfExchange.BusinessLayer.Checkers
{
    public class EndorsementChecker : IEndorsementChecker
    {
        public EndorsementCheckResult CheckList(BillOfExchange billOfExchange, IEnumerable<Endorsement> endorsemetList)
        {
            if (endorsemetList.Any(e => e.BillId != billOfExchange.Id))
            {
                throw new ArgumentException($"There is record in {nameof(endorsemetList)} which is not connected to {nameof(billOfExchange)}");
            }

            if (!endorsemetList.Any())
            {
                return new EndorsementCheckResult(true);
            }

            var first = endorsemetList.Where(e => e.PreviousEndorsementId == null).ToList();
            if (first.Count == 0)
            {
                return new EndorsementCheckResult(false, "There is no first endorsement");
            }

            if (first.Count > 1)
            {
                return new EndorsementCheckResult(false, "There is too many starting endorsement", first);
            }

            Endorsement current = first.First();
            if (current.NewBeneficiaryId == billOfExchange.BeneficiaryId)
            {
                return new EndorsementCheckResult(false, $"Endorsement with id {current.Id} have same BeneficiaryId {current.NewBeneficiaryId} as billOfExchange.", current);
            }
            ISet<int> usedIds = new HashSet<int>();
            usedIds.Add(current.Id);
            do
            {
                Endorsement next = endorsemetList.FirstOrDefault(e => current.Id == e.PreviousEndorsementId);
                if (next is null)
                {
                    break;
                }
                if (next.NewBeneficiaryId == current.NewBeneficiaryId)
                {
                    return new EndorsementCheckResult(false, $"There is same BeneficiaryId {next.NewBeneficiaryId} in two endorsement ids {current.Id} and {next.Id}", current, next);
                }
                if (usedIds.Contains(next.Id))
                {
                    return new EndorsementCheckResult(false, $"Endorsement with id {next.Id} is used.", next);
                }
                usedIds.Add(next.Id);

                current = next;
            } while (true);

            if (usedIds.Count != endorsemetList.Count())
            {
                return new EndorsementCheckResult(false, "Tehere is endorsement in endrosementList which is not in the sequence.", endorsemetList.Where(e => !usedIds.Contains(e.Id)).ToArray());
            }

            return new EndorsementCheckResult(true);
        }
    }
}
