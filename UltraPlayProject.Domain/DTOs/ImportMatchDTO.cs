using System.Xml.Serialization;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Match")]
    public class ImportMatchDTO
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("ID")]
        public int Id { get; set; }

        [XmlAttribute("StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute("MatchType")]
        public MatchType MatchType { get; set; }

        [XmlArray("Bet")]
        public ImportBetDTO[] Bets { get; set; }
    }
}
