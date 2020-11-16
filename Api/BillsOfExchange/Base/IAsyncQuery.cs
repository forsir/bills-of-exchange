using System.Threading;
using System.Threading.Tasks;

namespace BillsOfExchange.Base
{
    /// <summary>
    /// Query - asynchronní operace s Modelem
    /// </summary>
    /// <typeparam name="T">Typ parametru</typeparam>
    /// <typeparam name="TResult">Návratový typ</typeparam>
    public interface IAsyncQuery<in T, TResult>
    {
        /// <summary>Asynchronní volání Query</summary>
        /// <param name="parameter">Parametr</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TResult> ExecuteAsync(T parameter, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Query - asynchronní Execute se dvěma parametry
    /// </summary>
    /// <typeparam name="T1">Typ parametru 1</typeparam>
    /// <typeparam name="T2">Typ parametru 2</typeparam>
    /// <typeparam name="TResult">Návratový typ</typeparam>
    public interface IAsyncQuery<in T1, in T2, TResult>
    {
        /// <summary>Asynchronní volání Query</summary>
        /// <param name="parameter1">Parametr 1</param>
        /// <param name="parameter2">Parametr 2</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TResult> ExecuteAsync(T1 parameter1, T2 parameter2, CancellationToken cancellationToken = default);
    }
}