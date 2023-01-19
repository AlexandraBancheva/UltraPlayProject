using UltraPlayProject.Domain.DTOs.ExportDTOs;
using UltraPlayProject.Domain.Entities;
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
            var matches = _ultraPlayRepository.GetAllMatchesNext24Hours();
            //var matchesWithSpecialBetValue = new List<Match>();

            //foreach (var match in matches)
            //{
            //    foreach (var bet in match.Bets)
            //    {
            //        var isHasSpecialBetValue = bet.Odds.Where(o => o.SpecialBetValue != 0);
            //        if (isHasSpecialBetValue != null)
            //        {
            //            matchesWithSpecialBetValue.Add(match);
            //        }
            //    }
            //}

            return null;
        }
    }
}
