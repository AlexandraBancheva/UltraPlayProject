using System.Xml.Serialization;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Event")]
    public class ImportEventDTO
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("ID")]
        public int Id { get; set; }

        [XmlElement("IsLive")]
        public bool IsLive { get; set; }

        [XmlElement("CategoryID")]
        public int CategoryId { get; set; }

        [XmlArray("Match")]
        public ImportMatchDTO[] Matches { get; set; }
    }
}
