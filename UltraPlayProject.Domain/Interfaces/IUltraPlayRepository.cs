using UltraPlayProject.Domain.DTOs.ExportDTOs;
using UltraPlayProject.Domain.Entities;

namespace UltraPlayProject.Domain.Interfaces
{
    public interface IUltraPlayRepository
    {
        void GetDataFromXmlFile();

        List<ExportMatchDTO> GetAllMatchesNext24Hours();

        ExportMatchByIdDTO GetAllMatchesById(int id);

        void AddData();
    }
}
