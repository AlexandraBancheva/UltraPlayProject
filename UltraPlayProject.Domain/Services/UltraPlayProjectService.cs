using UltraPlayProject.Domain.DTOs.ExportDTOs;
using UltraPlayProject.Domain.Interfaces;

namespace UltraPlayProject.Domain.Services
{
    public class UltraPlayProjectService : IUltraPlayProjectService
    {
        public readonly IUltraPlayRepository _ultraPlayRepository;

        public UltraPlayProjectService(IUltraPlayRepository ultraPlayRepository)
        {
            _ultraPlayRepository = ultraPlayRepository;
        }

        public List<ExportMatchDTO> GetAllMarkets24Hours()
        {
            throw new NotImplementedException();
        }
    }
}
