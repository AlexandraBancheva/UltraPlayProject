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
            var uri = "https://sports.ultraplay.net/sportsxml?clientKey=9C5E796D-4D54-42FD-A535-D7E77906541A&sportId=2357&days=7";

            var reader = new StringReader(uri);
            var serializer = new XmlSerializer(typeof(ImportSportDTO[]), new XmlRootAttribute("Sport"));
            var sportDto = (ImportSportDTO[])serializer.Deserialize(reader);

            var sports = new List<Sport>();
            var events = new List<Event>();
            var matches = new List<Match>();
            var bets = new List<Bet>();
            var odds = new List<Odd>();

            foreach (var dto in sportDto)
            {
                // Sport
                var sport = new Sport
                {
                    Id = dto.Id,
                    Name = dto.Name,
                };

                // Events
                var eventz = dto.Events
                    .Where(e => db.Events.Any(ev => ev.Id == e.Id))
                    .Distinct();

                //Matches
                foreach (var match in eventz)
                {
                    var matchez = match.Matches.Where(m => db.Matches.Any(ma => ma.Id == m.Id)).Distinct();
                }
            }
        }
    }
}
