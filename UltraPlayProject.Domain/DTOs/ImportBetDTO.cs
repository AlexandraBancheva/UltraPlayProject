using System.Xml.Serialization;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Bet")]
    public class ImportBetDTO
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("ID")]
        public int Id { get; set; }

        [XmlElement("IsLive")]
        public bool IsLive { get; set; }

        [XmlArray("Odd")]
        public ImportOddDTO[] Odds { get; set; }
    }
}
