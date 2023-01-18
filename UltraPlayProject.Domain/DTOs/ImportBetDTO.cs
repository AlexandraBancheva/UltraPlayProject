using System.Xml.Serialization;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Bet")]
    public class ImportBetDTO
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlElement("Odd")]
        public ImportOddDTO[] Odds { get; set; }
    }
}
