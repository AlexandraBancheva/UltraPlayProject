using Microsoft.Extensions.Logging;
using UltraPlayProject.Domain.Interfaces;

namespace UltraPlayProject.Persistence.PeriodicBackgroundTask
{
    public class UpdatingDatabase
    {
        private readonly ILogger<UpdatingDatabase> _logger;
        private IUltraPlayRepository _ultraPlayRepository;

        public UpdatingDatabase(ILogger<UpdatingDatabase> logger, IUltraPlayRepository ultraPlayRepository)
        {
            _logger = logger;
            _ultraPlayRepository = ultraPlayRepository;
        }

        public async Task PopulateDatabase()
        {
            await Task.Delay(6000);
            _ultraPlayRepository.GetDataFromXmlFile();
            _logger.LogInformation("Updating database.");
        }
    }
}
