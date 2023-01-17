using System.Xml.Serialization;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Match")]
    public class ImportMatchDTO
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("ID")]
        public int Id { get; set; }

        [XmlElement("StartDate")]
        public DateTime StartDate { get; set; }

        [XmlElement("MatchType")]
        public MatchType MatchType { get; set; }

        [XmlArray("Bet")]
        public ImportBetDTO[] Bets { get; set; }
    }
}
