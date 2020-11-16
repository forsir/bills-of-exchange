using System.Diagnostics;
using Microsoft.Extensions.Logging;
using SimpleProxy;
using SimpleProxy.Interfaces;

namespace BillsOfExchange.Attributes
{
    /// <summary>
    /// IMethodInterceptor implementace pro logování metod (SimpleProxy)
    /// </summary>
    public class LogMethodInterceptor : IMethodInterceptor
    {
        private readonly Stopwatch stopwatch;
        private readonly ILogger<LogMethodInterceptor> logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        public LogMethodInterceptor(
            ILogger<LogMethodInterceptor> logger
        )
        {
            this.stopwatch = new Stopwatch();
            this.logger = logger;
        }

        /// <summary>
        /// Metoda vyvolaná na začátku volání logované metody
        /// </summary>
        /// <param name="invocationContext"></param>
        public void BeforeInvoke(InvocationContext invocationContext)
        {
            this.stopwatch.Start();

            this.logger.LogInformation($"*** {invocationContext.GetOwningType().FullName} executing: {invocationContext.GetExecutingMethodName()}");
        }

        /// <summary>
        /// Metoda vyvolaná na konci volání logované metody
        /// </summary>
        /// <param name="invocationContext"></param>
        /// <param name="methodResult"></param>
        public void AfterInvoke(InvocationContext invocationContext, object methodResult)
        {
            this.stopwatch.Stop();
            this.logger.LogInformation($"*** {invocationContext.GetOwningType().FullName}.{invocationContext.GetExecutingMethodName()} executed with result: {invocationContext.GetMethodReturnValue().GetType().Name} in {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}