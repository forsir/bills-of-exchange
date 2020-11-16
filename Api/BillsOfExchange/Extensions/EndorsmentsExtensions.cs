using System;
using System.Collections.Generic;
using System.Linq;
using BillsOfExchange.Exceptions;

namespace BillsOfExchange.Extensions
{
    /// <summary>
    /// Endorsments Extensions
    /// </summary>
    public static class EndorsmentsExtensions
    {
        /// <summary>
        /// Seřadí rubopisy od nejnovějšího po nejstarší dle hierarchie
        /// </summary>
        /// <param name="endorsements"></param>
        /// <returns></returns>
        public static IEnumerable<Models.Endorsment> SequenceSort(this IEnumerable<Models.Endorsment> endorsements)
        {
            if (endorsements == null)
            {
                throw new ArgumentNullException(nameof(endorsements));
            }

            if (!endorsements.Any())
            {
                return endorsements;
            }

            if (!endorsements.Any(t => !t.PreviousEndorsementId.HasValue))
            {
                throw new InvalidOperationException($"Směnka s ID = {endorsements.First().BillId} obsahuje rubopisy, nelze však určit první.");
            }

            if (endorsements.Count(t => !t.PreviousEndorsementId.HasValue) > 1)
            {
                throw new InvalidOperationException($"Směnka s ID = {endorsements.First().BillId} obsahuje více rubopisů, které jsou považované za první.");
            }

            var duplicity = endorsements.GroupBy(t => t.Id).Where(g => g.Count() > 1);

            if (duplicity.Any())
            {
                throw new InvalidOperationException($"Sada rubopisů na směnce s ID = {endorsements.First().BillId} obsahuje více rubopisů se stejným ID = {string.Join(' ', duplicity.Select(t => $"ID = {t.Key}"))}.");
            }

            if (endorsements.Count(t => !t.PreviousEndorsementId.HasValue) > 1)
            {
                throw new InvalidOperationException($"Směnka s ID = {endorsements.First().BillId} obsahuje více rubopisů, které jsou považované za první.");
            }

            if (endorsements.Any(t => t.PreviousEndorsementId == t.Id))
            {
                throw new InvalidOperationException($"Směnka s ID = {endorsements.First().BillId} obsahuje rubopisy, které odkazují samy na sebe {string.Join(' ', endorsements.Where(t => t.PreviousEndorsementId == t.Id).Select(t => $"ID = {t.Id}"))}.");
            }

            var sequenceArray = new Models.Endorsment[endorsements.Count()];

            sequenceArray[^1] = endorsements.First(t => !t.PreviousEndorsementId.HasValue);

            for (var i = sequenceArray.Length - 2; i >= 0; i--)
            {
                var seq = endorsements.Where(t => t.PreviousEndorsementId == sequenceArray[i + 1].Id);

                if (!seq.Any())
                {
                    throw new SequenceInteruptedException($"Směnka s ID = {endorsements.First().BillId} má chybnou posloupnost. Rubopis s PreviousEndorsementId = {sequenceArray[i + 1].Id} nebyl nalezen.");
                }

                if (seq.Count() > 1)
                {
                    throw new SequenceInteruptedException($"Směnka s ID = {endorsements.First().BillId} má chybnou posloupnost. Rubopis s PreviousEndorsementId = {sequenceArray[i + 1].Id} se na směnce nachází více než jednou.");
                }

                sequenceArray[i] = seq.First();
            }

            return sequenceArray.AsEnumerable();
        }
    }
}