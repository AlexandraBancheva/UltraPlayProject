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

        public List<ExportMatchDTO> GetAllMatchesStartingNext24H()
        {
            return _ultraPlayRepository.GetAllMatchesNext24Hours();
        }

        public ExportMatchByIdDTO GetMatchById(int id)
        {
            var expectedMatchById = _ultraPlayRepository.GetAllMatchesById(id);
            return expectedMatchById;
        }
    }
}
