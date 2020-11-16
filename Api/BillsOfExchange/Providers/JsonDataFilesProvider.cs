using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace BillsOfExchange.Providers
{
    /// <summary>
    /// Provider dat json souborů
    /// </summary>
    public class JsonDataFilesProvider : PhysicalFileProvider
    {
        /// <summary>
        /// Složka s json soubory
        /// </summary>
        public const string JsonFolder = "Data";

        /// <summary>
        /// Čas poslední modifikace json souborů
        /// </summary>
        public long MaxFilesModified { get; set; }

        private static string jsonRoot
        {
            get
            {
                var appRoot = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location);
                return Path.Combine(appRoot, JsonFolder);
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public JsonDataFilesProvider() : this(jsonRoot)
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="root"></param>
        public JsonDataFilesProvider(string root) : this(root, ExclusionFilters.Sensitive)
        {
        }


        /// <inheritdoc />
        public JsonDataFilesProvider(string root, ExclusionFilters filters) : base(root, filters)
        {
            this.MaxFilesModified = this.getLastModified();
            this.registerWatch();
        }

        private void registerWatch()
        {
            this.Watch("*.json").RegisterChangeCallback(cb =>
            {
                this.MaxFilesModified = this.getLastModified();
                this.registerWatch();
            }, default);
        }

        private long getLastModified()
        {
            var maxModified = this.GetDirectoryContents("").ToList().Max(t => t.LastModified);

            return new DateTimeOffset(maxModified.Year, maxModified.Month, maxModified.Day, maxModified.Hour, maxModified.Minute, maxModified.Second, 0, maxModified.Offset).Ticks;
        }
    }
}