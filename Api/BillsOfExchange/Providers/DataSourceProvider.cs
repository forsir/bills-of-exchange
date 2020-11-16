using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BillsOfExchange.Helpers;
using BillsOfExchange.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace BillsOfExchange.Providers
{
    /// <summary>
    /// DataSource Provider - data z json souborů
    /// </summary>
    public class DataSourceProvider : IDataSourceProvider
    {
        private readonly IMemoryCache memoryCache;
        private readonly IFileProvider fileProvider;
        private readonly ILogger<DataSourceProvider> logger;

        private const string billsOfExchangeJsonFilename = "BillsOfExchange.json";
        private const string partiesJsonFilename = "Parties.json";
        private const string endorsementsJsonFilename = "Endorsements.json";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="memoryCache"></param>
        /// <param name="logger"></param>
        /// <param name="fileProvider"></param>
        public DataSourceProvider(
            IMemoryCache memoryCache,
            ILogger<DataSourceProvider> logger,
            IFileProvider fileProvider
        )
        {
            this.fileProvider = fileProvider;
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        /// <inheritdoc />
        public async Task<DataSource<Endorsment>> EndorsmentsDataSource(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var data = await this.memoryCache.GetOrCreateAsync($"{nameof(EndorsmentsDataSource)}",
                async entry =>
                {
                    var endorsementsJsonFile = this.fileProvider.GetFileInfo(endorsementsJsonFilename);
                    var partiesJsonFile = this.fileProvider.GetFileInfo(partiesJsonFilename);

                    if (!endorsementsJsonFile.Exists)
                    {
                        throw new FileNotFoundException($"Input file not found.", endorsementsJsonFile.PhysicalPath);
                    }

                    if (!partiesJsonFile.Exists)
                    {
                        throw new FileNotFoundException($"Input file not found.", partiesJsonFile.PhysicalPath);
                    }


                    var dataEndorsements = await DataProviderHelper.GetArrayDataFromJson<DataProvider.Models.Endorsement>(endorsementsJsonFile);

                    var changeTokenEndorsements = this.fileProvider.Watch(endorsementsJsonFile.Name);
                    changeTokenEndorsements.RegisterChangeCallback(cb => { this.logger.LogDebug($"{nameof(DataSourceProvider)}: Json file '{endorsementsJsonFile.Name}' was changed."); }, default);
                    entry.AddExpirationToken(changeTokenEndorsements);

                    var dataParties = await DataProviderHelper.GetArrayDataFromJson<DataProvider.Models.Party>(partiesJsonFile);

                    var changeTokenParties = this.fileProvider.Watch(partiesJsonFile.Name);
                    changeTokenParties.RegisterChangeCallback(cb => { this.logger.LogDebug($"{nameof(DataSourceProvider)}: Json file '{partiesJsonFile.Name}' was changed."); }, default);
                    entry.AddExpirationToken(changeTokenParties);

                    var result = dataEndorsements.Select(t => new Models.Endorsment()
                    {
                        Id = t.Id,
                        BillId = t.BillId,
                        NewBeneficiaryId = t.NewBeneficiaryId,
                        NewBeneficiary = dataParties.Count(p => p.Id == t.NewBeneficiaryId) == 1
                            ? dataParties.Where(p => p.Id == t.NewBeneficiaryId).Select(p => new Models.Party()
                            {
                                Name = p.Name,
                                Id = p.Id
                            }).First()
                            : null,
                        PreviousEndorsementId = t.PreviousEndorsementId
                    });

                    return new DataSource<Models.Endorsment>()
                    {
                        Count = result.Count(),
                        Data = result
                    };
                });

            return data;
        }

        /// <inheritdoc />
        public async Task<DataSource<Models.BillOfExchange>> BillsOfExchangeDataSource(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var data = await this.memoryCache.GetOrCreateAsync($"{nameof(BillsOfExchangeDataSource)}",
                async entry =>
                {
                    var billsOfExchangeJsonFile = this.fileProvider.GetFileInfo(billsOfExchangeJsonFilename);
                    var partiesJsonFile = this.fileProvider.GetFileInfo(partiesJsonFilename);

                    if (!billsOfExchangeJsonFile.Exists)
                    {
                        throw new FileNotFoundException($"Input file not found.", billsOfExchangeJsonFile.PhysicalPath);
                    }

                    if (!partiesJsonFile.Exists)
                    {
                        throw new FileNotFoundException($"Input file not found.", partiesJsonFile.PhysicalPath);
                    }


                    var dataBillsOfExchange = await DataProviderHelper.GetArrayDataFromJson<DataProvider.Models.BillOfExchange>(billsOfExchangeJsonFile);

                    var changeTokenBillsOfExchange = this.fileProvider.Watch(billsOfExchangeJsonFile.Name);
                    changeTokenBillsOfExchange.RegisterChangeCallback(cb => { this.logger.LogDebug($"{nameof(DataSourceProvider)}: Json file '{billsOfExchangeJsonFile.Name}' was changed."); }, default);
                    entry.AddExpirationToken(changeTokenBillsOfExchange);

                    var dataParties = await DataProviderHelper.GetArrayDataFromJson<DataProvider.Models.Party>(partiesJsonFile);

                    var changeTokenParties = this.fileProvider.Watch(partiesJsonFile.Name);
                    changeTokenParties.RegisterChangeCallback(cb => { this.logger.LogDebug($"{nameof(DataSourceProvider)}: Json file '{partiesJsonFile.Name}' was changed."); }, default);
                    entry.AddExpirationToken(changeTokenParties);

                    var result = dataBillsOfExchange.Select(t => new Models.BillOfExchange()
                    {
                        Id = t.Id,
                        DrawerId = t.DrawerId,
                        BeneficiaryId = t.BeneficiaryId,
                        Drawer = dataParties.Count(p => p.Id == t.DrawerId) == 1
                            ? dataParties.Where(p => p.Id == t.DrawerId).Select(p => new Models.Party()
                            {
                                Name = p.Name,
                                Id = p.Id
                            }).First()
                            : null,
                        Beneficiary = dataParties.Count(p => p.Id == t.BeneficiaryId) == 1
                            ? dataParties.Where(p => p.Id == t.BeneficiaryId).Select(p => new Models.Party()
                            {
                                Name = p.Name,
                                Id = p.Id
                            }).First()
                            : null
                    });

                    return new DataSource<Models.BillOfExchange>()
                    {
                        Count = result.Count(),
                        Data = result
                    };
                });

            return data;
        }

        /// <inheritdoc />
        public async Task<DataSource<Party>> PartiesDataSource(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var data = await this.memoryCache.GetOrCreateAsync($"{nameof(BillsOfExchangeDataSource)}",
                async entry =>
                {
                    var partiesJsonFile = this.fileProvider.GetFileInfo(partiesJsonFilename);

                    if (!partiesJsonFile.Exists)
                    {
                        throw new FileNotFoundException($"Input file not found.", partiesJsonFile.PhysicalPath);
                    }

                    var dataParties = await DataProviderHelper.GetArrayDataFromJson<DataProvider.Models.Party>(partiesJsonFile);

                    var changeTokenParties = this.fileProvider.Watch(partiesJsonFile.Name);
                    changeTokenParties.RegisterChangeCallback(cb => { this.logger.LogDebug($"{nameof(DataSourceProvider)}: Json file '{partiesJsonFile.Name}' was changed."); }, default);
                    entry.AddExpirationToken(changeTokenParties);

                    var result = dataParties.Select(t => new Models.Party()
                    {
                        Id = t.Id,
                        Name = t.Name
                    });

                    return new DataSource<Models.Party>()
                    {
                        Count = result.Count(),
                        Data = result
                    };
                });

            return data;
        }
    }
}