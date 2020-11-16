using System;
using BillsOfExchange.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace BillsOfExchange.Extensions
{
    /// <summary>
    /// Kestrel Extensions
    /// </summary>
    public static class KestrelExtensions
    {
        /// <summary>
        /// Nastaví defaultní hodnoty Kestrelu 
        /// </summary>
        /// <param name="targetPort">Port na kterém bude aplikace dostupná (default: 5000)</param>
        /// <param name="allowSynchronousIo">Povolí async operace (default: true)</param>
        /// <param name="maxRequestBodySize">Limit velikosti přijímaných dat (default: null)</param>
        /// <param name="requestHeadersTimeout">Limit timeoutu získávání HTTP hlaviček (default: 600s)</param>
        /// <param name="keepAliveTimeout">Nastavení KeepAlive (default: 600s)</param>
        /// <param name="maxStreamsPerConnection">Limit paralelních streamů v připojení</param>
        /// <returns></returns>
        public static Action<WebHostBuilderContext, KestrelServerOptions> DefaultKestrelConfig(
            int targetPort = 5000,
            bool allowSynchronousIo = true,
            long? maxRequestBodySize = null,
            int requestHeadersTimeout = 600,
            int keepAliveTimeout = 600,
            int maxStreamsPerConnection = 1000
        )
        {
            return (context, options) =>
            {
                var targetUrl = Environment.GetEnvironmentVariable(EnvironmentKeys.AspNetCoreUrls);
                if (!string.IsNullOrEmpty(targetUrl))
                {
                    if (Uri.TryCreate(targetUrl, UriKind.RelativeOrAbsolute, out var uri))
                    {
                        targetPort = uri.Port;
                    }
                }

                options.AllowSynchronousIO = allowSynchronousIo;
                options.AddServerHeader = false;
                options.Limits.MaxRequestBodySize = maxRequestBodySize;
                options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(requestHeadersTimeout);
                options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(keepAliveTimeout);
                options.Limits.Http2.MaxStreamsPerConnection = maxStreamsPerConnection;
                options.Limits.Http2.HeaderTableSize = 4096;
                options.Limits.Http2.MaxFrameSize = 16384;
                options.Limits.Http2.MaxRequestHeaderFieldSize = 8192;

                options.ListenAnyIP(targetPort,
                    listenOptions => { listenOptions.Protocols = HttpProtocols.Http1AndHttp2; });
            };
        }
    }
}