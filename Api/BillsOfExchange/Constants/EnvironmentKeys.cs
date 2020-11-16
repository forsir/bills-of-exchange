namespace BillsOfExchange.Constants
{
    /// <summary>
    /// Konstanty Environment prostředí
    /// </summary>
    public static class EnvironmentKeys
    {
        /// <summary>
        /// Nastavení URL pro běh aplikace
        /// </summary>
        public const string AspNetCoreUrls = "ASPNETCORE_URLS";

        /// <summary>
        /// Nastavení použití aktivního způsobu sledování změn filesystému
        /// </summary>
        public const string UsePollingFileWatcher = "DOTNET_USE_POLLING_FILE_WATCHER";
    }
}