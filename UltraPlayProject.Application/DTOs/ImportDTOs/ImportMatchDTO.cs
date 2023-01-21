using System.Xml.Serialization;

namespace UltraPlayProject.Application.DTOs.ImportDTOs
{
    [XmlType("Match")]
    public class ImportMatchDTO
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlAttribute("StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute("MatchType")]
        public string MatchType { get; set; }

        [XmlElement("Bet")]
        public ImportBetDTO[] Bets { get; set; }
    }
}
