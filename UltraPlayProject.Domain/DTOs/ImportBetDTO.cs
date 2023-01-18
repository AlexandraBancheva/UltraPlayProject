using System.Xml.Serialization;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Bet")]
    public class ImportBetDTO
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("ID")]
        public int Id { get; set; }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlArray("Odd")]
        public ImportOddDTO[] Odds { get; set; }
    }
}
