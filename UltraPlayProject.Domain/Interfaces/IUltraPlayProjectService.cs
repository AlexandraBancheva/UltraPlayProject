using UltraPlayProject.Domain.DTOs.ExportDTOs;

namespace UltraPlayProject.Domain.Interfaces
{
    public interface IUltraPlayProjectService
    {
        List<ExportMatchDTO> GetAllMarkets24Hours();
    }
}
