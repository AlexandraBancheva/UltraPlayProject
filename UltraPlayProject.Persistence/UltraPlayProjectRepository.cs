using System.Net;
using System.Text;
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

            foreach (var dto in sportDto)
            {
                var sport = new Sport
                {
                    Id = dto.Id,
                    Name = dto.Name,
                };
            }
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
