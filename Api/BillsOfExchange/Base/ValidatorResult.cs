using System.Collections.Generic;

namespace BillsOfExchange.Base
{
    /// <summary>
    /// Vásledek validace
    /// </summary>
    public class ValidatorResult
    {
        /// <summary>
        /// Obsahuje chyby
        /// </summary>
        public bool HasError { get; private set; }

        /// <summary>
        /// Kolekce chyb
        /// </summary>
        public IEnumerable<string> ValidationErrors { get; } = new List<string>();

        /// <summary>
        /// Přidá validační chybu do resultu
        /// </summary>
        /// <param name="error">Message</param>
        public void SetError(string error)
        {
            this.HasError = true;
            ((List<string>) this.ValidationErrors).Add(error);
        }
    }
}