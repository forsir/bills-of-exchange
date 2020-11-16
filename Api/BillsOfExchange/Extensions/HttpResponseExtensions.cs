using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace BillsOfExchange.Extensions
{
    /// <summary>
    /// HttpResponse Extensions
    /// </summary>
    public static class HttpResponseExtensions
    {
        private static readonly JsonSerializer serializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Zapíše objekt jako json do response
        /// </summary>
        /// <typeparam name="T">Typ vstupního objektu</typeparam>
        /// <param name="response">Http response</param>
        /// <param name="obj"></param>
        /// <param name="contentType"></param>
        public static void WriteJson<T>(this HttpResponse response, T obj, string contentType = null)
        {
            response.ContentType = contentType ?? "application/json";
            using var writer = new HttpResponseStreamWriter(response.Body, Encoding.UTF8);
            using var jsonWriter = new JsonTextWriter(writer)
            {
                CloseOutput = false,
                AutoCompleteOnClose = false
            };

            serializer.Serialize(jsonWriter, obj);
        }
    }
}