using System.Threading;
using System.Threading.Tasks;

namespace BillsOfExchange.Base
{
    /// <summary>
    /// RequestHandler - asynchronní operace s Contractem
    /// </summary>
    /// <typeparam name="T">Typ parametru</typeparam>
    /// <typeparam name="TResult">Návratový typ</typeparam>
    public interface IAsyncRequestHandler<in T, TResult>
    {
        /// <summary>Asynchronní volání RequestHandleru</summary>
        /// <param name="parameter">Parametr</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TResult> ExecuteAsync(T parameter, CancellationToken cancellationToken = default);
    }
}