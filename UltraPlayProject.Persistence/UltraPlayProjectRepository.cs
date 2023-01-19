using System.Net;
using System.Text;
using System.Xml.Serialization;
using UltraPlayProject.Domain.DTOs;
using UltraPlayProject.Domain.DTOs.ExportDTOs;
using UltraPlayProject.Domain.Entities;
using UltraPlayProject.Domain.Interfaces;

namespace UltraPlayProject.Persistence
{
    public class UltraPlayProjectRepository : IUltraPlayRepository
    {
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
            ClearDatabase(db);
            FillTheDatabase(db, sportDto, sports);
        }

        private void FillTheDatabase(UltraPlayProjectContext db, ImportSportDTO[]? sportDto, List<Sport> sports)
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
                        Enum.TryParse(match.MatchType, out MatchType matchType);

                        var mtch = new Match
                        {
                            ID = match.ID,
                            Name = match.Name,
                            StartDate = match.StartDate,
                            MatchType = matchType,
                        };


                        foreach (var bet in match.Bets)
                        {
                            // BET
                            var bt = new Bet
                            {
                                ID = bet.ID,
                                Name = bet.Name,
                                IsLive = bet.IsLive,
                            };

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
            db.Sports.AddRange(sports);
            db.SaveChanges();
        }

        private void ClearDatabase(UltraPlayProjectContext db)
        {
            db.Odds.RemoveRange(db.Odds.ToList());
            db.Bets.RemoveRange(db.Bets.ToList());
            db.Matches.RemoveRange(db.Matches.ToList());
            db.Events.RemoveRange(db.Events.ToList());
            db.Sports.RemoveRange(db.Sports.ToList());
            db.SaveChanges();
        }

        public static string GetWebPage(Uri uri)
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


            var matchWithSpecialBetValue = new List<ExportMatchDTO>();
            foreach (var match in matches)
            {
                foreach (var bet in match.Markets)
                {
                    if (bet.IsLive)
                    {
                        matchWithSpecialBetValue.Add(match);
                    }
                }
            }
            return matchWithSpecialBetValue;
        }
    }
}
