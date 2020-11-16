using System;

namespace BillsOfExchange.Exceptions
{
    /// <summary>
    /// Chyba neintegrity posloupnosti
    /// </summary>
    public class SequenceInteruptedException : Exception
    {
        /// <inheritdoc />
        public SequenceInteruptedException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public SequenceInteruptedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc />
        public SequenceInteruptedException() : base()
        {
        }
    }
}