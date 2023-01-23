using System.Net;
using System.Text;
using System.Xml.Serialization;
using UltraPlayProject.Application.DTOs.ImportDTOs;
using UltraPlayProject.Application.Interfaces;
using UltraPlayProject.Domain.Entities;
using UltraPlayProject.Domain.Enum;

namespace UltraPlayProject.Persistence
{
    public class UltraPlayDatabaseRepository : IUltraPlayDatabaseRepository
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
            var savedIds = new List<int>();
            FillTheDatabase(db, sportDto, sports, savedIds);
        }

        private static void FillTheDatabase(UltraPlayProjectContext db, ImportSportDTO[]? sportDto, List<Sport> sports, List<int> savedIDs)
        {
            var matches = new List<Match>();
            var bets = new List<Bet>();
            var odds = new List<Odd>();

            foreach (var dto in sportDto)
            {
                var sport = new Sport
                {
                    ID = dto.ID,
                    Name = dto.Name,
                };

                var eventz = dto.Events;

                foreach (var even in eventz)
                {
                    var evnt = new Event
                    {
                        ID = even.ID,
                        Name = even.Name,
                        IsLive = even.IsLive,
                        CategoryID = even.CategoryID,
                    };

                    foreach (var match in even.Matches)
                    {
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
                            var bt = new Bet
                            {
                                ID = bet.ID,
                                Name = bet.Name,
                                IsLive = bet.IsLive,
                            };
                            savedIDs.Add(bt.ID);
                            foreach (var odd in bet.Odds)
                            {
                                var dd = new Odd
                                {
                                    ID = odd.ID,
                                    Name = odd.Name,
                                    Value = odd.Value,
                                    SpecialBetValue = odd.SpecialBetValue != null ? odd.SpecialBetValue : null,
                                };
                                odds.Add(dd);
                                savedIDs.Add(dd.ID);
                                bt.Odds.Add(dd);
                            }
                            bets.Add(bt);
                            mtch.Bets.Add(bt);
                        }
                        matches.Add(mtch);
                        evnt.Matches.Add(mtch);
                    }
                    sport.Events.Add(evnt);
                }
                sports.Add(sport);
            }

            CheckForRemovedEntities(db, savedIDs, matches, bets, odds);
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

        private static void CheckForRemovedEntities(UltraPlayProjectContext db, List<int> savedIDs, List<Match> matches, List<Bet> bets, List<Odd> odds)
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
                AddMessagesForRemovedEntities(db, removedIds);
            }
            
            CheckForUpdatingEntities(db, existedIds, matches, bets, odds);
        }

        private static void CheckForUpdatingEntities(UltraPlayProjectContext db, List<int> existedIds, List<Match> matches, List<Bet> bets, List<Odd> odds)
        {
            var logs = new List<DatabaseLog>();
            foreach (var existedId in existedIds)
            {
                var newMatch = matches.Where(m => m.ID == existedId).FirstOrDefault();
                var newOdd = odds.Where(o => o.ID == existedId).FirstOrDefault();


                if (newMatch != null)
                {
                    var oldMatch = db.Matches.FirstOrDefault(m => m.ID == newMatch.ID);
                    if (oldMatch.StartDate != newMatch.StartDate)
                    {
                        logs.Add(new DatabaseLog
                        {
                            Date = DateTime.Now,
                            Message = "Match.StartDate was changed.",
                        });
                    }
                    else if (oldMatch.MatchType != newMatch.MatchType)
                    {
                        logs.Add(new DatabaseLog
                        {
                            Date = DateTime.Now,
                            Message = "Match.MatchType was changed.",
                        });
                    }
                }
                else if (newOdd != null)
                {
                    var oldOdd = db.Odds.FirstOrDefault(o => o.ID == newOdd.ID);
                    if (oldOdd.Value != newOdd.Value)
                    {
                        logs.Add(new DatabaseLog
                        {
                            Date = DateTime.Now,
                            Message = "Odd.Value was changed.",
                        });
                    }
                }
            }
            AddLogsForUpdatingEntitiesToDatabase(db, logs);
        }

        private static void AddMessagesForRemovedEntities(UltraPlayProjectContext db, List<int> removedIds)
        {
            foreach (var id in removedIds)
            {
                var info = new DatabaseLog
                {
                    Date = DateTime.Now,
                    Message = $"Entity with {id} was removed from database."
                };
                db.DatabaseLogs.Add(info);
                db.SaveChanges();
            }
        }

        private static void AddLogsForUpdatingEntitiesToDatabase(UltraPlayProjectContext db, List<DatabaseLog> logs)
        {
            db.DatabaseLogs.AddRange(logs);
            db.SaveChanges();
        }
    }
}
