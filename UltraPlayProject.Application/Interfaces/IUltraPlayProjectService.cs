using UltraPlayProject.Domain.DTOs.ExportDTOs;

namespace UltraPlayProject.Domain.Interfaces
{
    public interface IUltraPlayProjectService
    {
        List<ExportMatchDTO> GetAllMatchesStartingNext24H();

        ExportMatchByIdDTO GetMatchById(int id);
    }
}
