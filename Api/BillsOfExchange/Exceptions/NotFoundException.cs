using System;

namespace BillsOfExchange.Exceptions
{
    /// <summary>
    /// Chyba nenalezeno
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <inheritdoc />
        public NotFoundException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc />
        public NotFoundException()
        {
        }
    }
}