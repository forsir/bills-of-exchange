namespace BillsOfExchange.Base
{
    /// <summary>
    /// Mapuje objekty typu <typeparamref name="TSource" /> na <typeparamref name="TDestination" />.
    /// </summary>
    /// <typeparam name="TSource">Typ zdrojového objektu</typeparam>
    /// <typeparam name="TDestination">Typ cílového objektu</typeparam>
    public interface IMapper<in TSource, in TDestination>
    {
        /// <summary>
        /// Mapuje zdroj na cíl
        /// </summary>
        /// <param name="source">Zdrojový objekt</param>
        /// <param name="destination">Cílový objekt</param>
        void Map(TSource source, TDestination destination);
    }
}