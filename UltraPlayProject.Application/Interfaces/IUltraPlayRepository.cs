using UltraPlayProject.Domain.DTOs.ExportDTOs;

namespace UltraPlayProject.Domain.Interfaces
{
    public interface IUltraPlayRepository
    {
        List<ExportMatchDTO> GetAllMatchesNext24Hours();

        ExportMatchByIdDTO GetAllMatchesById(int id);
    }
}
