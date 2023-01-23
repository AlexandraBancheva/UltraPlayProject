using Microsoft.Extensions.Logging;
using UltraPlayProject.Application.Interfaces;

namespace UltraPlayProject.Persistence.PeriodicBackgroundTask
{
    public class UpdatingDatabase
    {
        private readonly ILogger<UpdatingDatabase> _logger;
        private readonly IUltraPlayDatabaseRepository _ultraPlayDatabaseRepository;

        public UpdatingDatabase(ILogger<UpdatingDatabase> logger, IUltraPlayDatabaseRepository ultraPlayDatabaseRepository)
        {
            _logger = logger;
            _ultraPlayDatabaseRepository = ultraPlayDatabaseRepository;
        }

        public async Task PopulateDatabase()
        {
            await Task.Delay(100);
            _ultraPlayDatabaseRepository.GetDataFromXmlFile();
            _logger.LogInformation("Updating database.");
        }
    }
}
