using System.Net;
using System.Text;
using System.Xml.Serialization;
using UltraPlayProject.Application.DTOs.ImportDTOs;
using UltraPlayProject.Domain.DTOs.ExportDTOs;
using UltraPlayProject.Domain.Entities;
using UltraPlayProject.Domain.Enum;
using UltraPlayProject.Domain.Interfaces;

namespace UltraPlayProject.Persistence
{
    public class UltraPlayProjectRepository : IUltraPlayRepository
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
            foreach (var match in matches)
            {
                foreach (var bet in match.Markets)
                {
                    if ((bet.Name == "Match Winner" || bet.Name == "Map Advantage" || bet.Name == "Total Maps Played") && bet.IsLive)
                    {
                        //if (bet.Odds.All(o => o.SpecialBetValue != 0))
                        //{
                        //}
                        activeMatches.Add(match);
                    }
                }
            }

            var matchesResult = new List<ExportMatchDTO>();


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

        public void GetDataFromXmlFile()
        {
            var db = new UltraPlayProjectContext();
            Uri uri = new Uri("https://sports.ultraplay.net/sportsxml?clientKey=9C5E796D-4D54-42FD-A535-D7E77906541A&sportId=2357&days=7");
            var downloadedXmlFile = GetWebPage(uri);


            var reader = new StringReader(downloadedXmlFile);
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "XmlSports";
            xRoot.IsNullable = true;
            var serializer = new XmlSerializer(typeof(ImportSportDTO[]), xRoot);
            var sportDto = (ImportSportDTO[])serializer.Deserialize(reader);

            var sports = new List<Sport>();
            var savedIds = new List<int>(); 
            FillTheDatabase(db, sportDto, sports, savedIds);
        }

        private static void FillTheDatabase(UltraPlayProjectContext db, ImportSportDTO[]? sportDto, List<Sport> sports, List<int> savedIDs)
        {
            foreach (var dto in sportDto)
            {
                //SPORT
                var sport = new Sport
                {
                    ID = dto.ID,
                    Name = dto.Name,
                };

                var eventz = dto.Events;

                foreach (var even in eventz)
                {
                    //EVENT
                    var evnt = new Event
                    {
                        ID = even.ID,
                        Name = even.Name,
                        IsLive = even.IsLive,
                        CategoryID = even.CategoryID,
                    };


                    foreach (var match in even.Matches)
                    {
                        //MATCH
                        Enum.TryParse(match.MatchType, out MatchTypeEvent matchType);

                        var mtch = new Match
                        {
                            ID = match.ID,
                            Name = match.Name,
                            StartDate = match.StartDate,
                            MatchType = matchType,
                        };
                        savedIDs.Add(mtch.ID);

                        foreach (var bet in match.Bets)
                        {
                            // BET
                            var bt = new Bet
                            {
                                ID = bet.ID,
                                Name = bet.Name,
                                IsLive = bet.IsLive,
                            };
                            savedIDs.Add(bt.ID);
                            foreach (var odd in bet.Odds)
                            {
                                //ODD
                                var dd = new Odd
                                {
                                    ID = odd.ID,
                                    Name = odd.Name,
                                    Value = odd.Value,
                                    SpecialBetValue = odd.SpecialBetValue != null ? odd.SpecialBetValue : null,
                                };
                                savedIDs.Add(dd.ID);
                                bt.Odds.Add(dd);
                            }
                            mtch.Bets.Add(bt);
                        }
                        evnt.Matches.Add(mtch);
                    }
                    sport.Events.Add(evnt);
                }
                sports.Add(sport);
            }

            CheckForRemovedIds(db, savedIDs);
            ClearDatabase(db);

            db.Sports.AddRange(sports);
            db.SaveChanges();
        }

        private static void ClearDatabase(UltraPlayProjectContext db)
        {
            db.Odds.RemoveRange(db.Odds.ToList());
            db.Bets.RemoveRange(db.Bets.ToList());
            db.Matches.RemoveRange(db.Matches.ToList());
            db.Events.RemoveRange(db.Events.ToList());
            db.Sports.RemoveRange(db.Sports.ToList());
            db.SaveChanges();
        }

        private static string GetWebPage(Uri uri)
        {
            if ((uri == null))
            {
                throw new ArgumentNullException("uri");
            }

            using (var request = new WebClient())
            {
                var requestData = request.DownloadData(uri);

                return Encoding.ASCII.GetString(requestData);
            }
        }

        private static void CheckForRemovedIds(UltraPlayProjectContext db, List<int> savedIDs) 
        {
            var removedIds = new List<int>();
            var allMatchIds = db.Matches.Where(m => db.Matches.Any(mt => mt.ID == m.ID)).Select(m => m.ID).ToList();
            var allBetIds = db.Bets.Where(b => db.Bets.Any(bt => bt.ID == b.ID)).Select(b => b.ID).ToList();
            var allOddIds = db.Odds.Where(o => db.Odds.Any(dd => dd.ID == o.ID)).Select(o => o.ID).ToList();
            removedIds.AddRange(allMatchIds);
            removedIds.AddRange(allBetIds);
            removedIds.AddRange(allOddIds);

            var existedIds = new List<int>();
            var newIds = new List<int>();

            foreach (var id in savedIDs)
            {
                if (allMatchIds.Contains(id) || allBetIds.Contains(id) || allOddIds.Contains(id))
                {
                    existedIds.Add(id);
                }
                else
                {
                    newIds.Add(id);
                }
            }

            foreach (var forRemove in existedIds)
            {
                removedIds.Remove(forRemove);
            }

            if (removedIds != null)
            {
                AddMessageToDatabase(db, removedIds);
            }
        }

        public static void AddMessageToDatabase(UltraPlayProjectContext db, List<int> removedIds)
        {
            foreach (var id in removedIds)
            {
                var info = new DatabaseLog
                {
                    Date = DateTime.Now,
                    Message = $"{id} was removed from database."
                };
                db.DatabaseLogs.Add(info);
                db.SaveChanges();
            }
        }
    }
}
