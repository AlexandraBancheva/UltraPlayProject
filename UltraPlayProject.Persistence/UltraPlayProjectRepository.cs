using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Transactions;
using System.Xml.Linq;
using System.Xml.Serialization;
using UltraPlayProject.Domain.DTOs;
using UltraPlayProject.Domain.Entities;
using UltraPlayProject.Domain.Interfaces;

namespace UltraPlayProject.Persistence
{
    public class UltraPlayProjectRepository : IUltraPlayRepository
    {
        public void UpdateDatabase()
        {
            var db = new UltraPlayProjectContext();
            Uri uri = new Uri("https://sports.ultraplay.net/sportsxml?clientKey=9C5E796D-4D54-42FD-A535-D7E77906541A&sportId=2357&days=7");
            var result =  GetWebPage(uri);


            var reader = new StringReader(result);
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "XmlSports";
            xRoot.IsNullable = true;
            var serializer = new XmlSerializer(typeof(ImportSportDTO[]), xRoot);
            var sportDto = (ImportSportDTO[])serializer.Deserialize(reader);

            var sports = new List<Sport>();
            var events = new List<Event>();
            var matches = new List<Match>();
            var bets = new List<Bet>();
            var odds = new List<Odd>();

            //db.Database.EnsureDeleted();

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
                                    SpecialBetValue = odd.SpecialBetValue,
                                };
                                bt.Odds.Add(dd);
                                odds.Add(dd);
                            }
                            bets.Add(bt);
                            mtch.Bets.Add(bt);
                        }
                        matches.Add(mtch);
                        evnt.Matches.Add(mtch);
                    }
                    events.Add(evnt);
                    sport.Events.Add(evnt);
                }

                sports.Add(sport);
            }
            db.Odds.AddRange(odds);
            db.Bets.AddRange(bets);
            db.Matches.AddRange(matches);
            db.Events.AddRange(events);
            db.Sports.AddRange(sports);
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
    }
}
