using UltraPlayProject.Domain.Entities;

namespace UltraPlayProject.Domain.Interfaces
{
    public interface IUltraPlayRepository
    {
        void UpdateDatabase();

        List<Match> GetAllMatchesLast24Hours();
    }
}
