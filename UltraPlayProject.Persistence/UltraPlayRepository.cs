using UltraPlayProject.Domain.DTOs.ExportDTOs;
using UltraPlayProject.Domain.Interfaces;

namespace UltraPlayProject.Persistence
{
    public class UltraPlayRepository : IUltraPlayRepository
    {
        public List<ExportMatchDTO> GetAllMatchesNext24Hours()
        {
            var db = new UltraPlayProjectContext();
            var matches = db.Matches
                .Where(m => m.StartDate <= DateTime.Now.AddHours(24))
                .Select(m => new ExportMatchDTO
                {
                    StartDate = m.StartDate,
                    Name = m.Name,
                    Markets = m.Bets
                                .Select(ma => new ExportMarketsDTO
                                {
                                    ID = ma.ID,
                                    Name = ma.Name,
                                    IsLive = ma.IsLive,
                                    Odds = ma.Odds.Select(o => new ExportOddsDTO
                                    {
                                        ID = o.ID,
                                        Name = o.Name,
                                        Value = o.Value,
                                        SpecialBetValue = o.SpecialBetValue,
                                    }).ToList()
                                }).ToList()
                }).ToList();


            var activeMatches = new List<ExportMatchDTO>();
            var groupingBySpecialBetValue = new List<ExportOddsDTO>();
            var firstGroup = new ExportOddsDTO();
            foreach (var match in matches)
            {
                foreach (var bet in match.Markets)
                {
                    if ((bet.Name.Equals("Match Winner") || bet.Name.Equals("Map Advantage") || bet.Name.Equals("Total Maps Played")) && bet.IsLive) 
                    {
                        if (bet.Odds.Any(o => o.SpecialBetValue != 0) && bet.Odds.Count > 2)
                        {
                            groupingBySpecialBetValue = bet.Odds.GroupBy(o => o.SpecialBetValue).SelectMany(group => group).ToList();
                            firstGroup = groupingBySpecialBetValue.FirstOrDefault();
                            bet.Odds.Clear();
                            bet.Odds.Add(firstGroup);
                        }
                        activeMatches.Add(match);
                    }
                }
            }
            return activeMatches;
        }

        public ExportMatchByIdDTO GetAllMatchesById(int id)
        {
            var db = new UltraPlayProjectContext();
            var matchById = db.Matches.Where(m => m.ID == id)
                .Select(m => new ExportMatchByIdDTO
                {
                    Name = m.Name,
                    StartDate = m.StartDate,
                    ActiveMarkets = m.Bets.Where(b => b.IsLive == true)
                                          .Select(ab => new ExportActiveMarketsByMatchId
                                          {
                                              Name = ab.Name,
                                              IsLive = ab.IsLive,
                                              ID = ab.ID,
                                              Odds = ab.Odds.Select(o => new ExportOddsDTO
                                              {
                                                  Name = o.Name,
                                                  ID = o.ID,
                                                  Value = o.Value,
                                                  SpecialBetValue = o.SpecialBetValue,
                                              }).ToList()
                                          }).ToList(),
                    InactiveMarkets = m.Bets.Where(b => b.IsLive == false)
                                            .Select(ib => new ExportInactiveMarketsByMatchIdDTO
                                            {
                                                Name = ib.Name,
                                                ID = ib.ID,
                                                IsLive = ib.IsLive,
                                                Odds = ib.Odds.Select(o => new ExportOddsDTO
                                                {
                                                    Name = o.Name,
                                                    ID = o.ID,
                                                    Value = o.Value,
                                                    SpecialBetValue = o.SpecialBetValue
                                                }).ToList()
                                            }).ToList()
                }).FirstOrDefault();

            return matchById;
        }
    }
}
