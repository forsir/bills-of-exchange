namespace BillsOfExchange.Base
{
    /// <summary>
    /// Validátor objektů
    /// </summary>
    /// <typeparam name="T">Typ entity, která je validována</typeparam>
    public interface IValidator<in T> where T : class
    {
        /// <summary>
        /// Validace objektu
        /// </summary>
        /// <param name="objectToValidate">Validovaný objekt</param>
        /// <returns></returns>
        ValidatorResult Validate(T objectToValidate);
    }
}