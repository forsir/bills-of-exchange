using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace BillsOfExchange.Helpers
{
    /// <summary>
    /// DataProvider Helper
    /// </summary>
    public static class DataProviderHelper
    {
        /// <summary>
        /// Deserializuje data z jsou souboru
        /// </summary>
        /// <typeparam name="T">Cílový typ</typeparam>
        /// <param name="jsonFile">FileInfo json souboru</param>
        /// <returns></returns>
        public static async ValueTask<IEnumerable<T>> GetArrayDataFromJson<T>(IFileInfo jsonFile)
        {
            if (jsonFile == null)
            {
                throw new ArgumentNullException(nameof(jsonFile));
            }

            if (!jsonFile.Exists)
            {
                throw new FileNotFoundException($"Input file not found.", jsonFile.PhysicalPath);
            }

            await using var jsonFs = jsonFile.CreateReadStream();
            return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(jsonFs);
        }
    }
}