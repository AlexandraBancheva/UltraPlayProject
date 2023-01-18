using System.Xml.Serialization;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Event")]
    public class ImportEventDTO
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("ID")]
        public int Id { get; set; }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlAttribute("CategoryID")]
        public int CategoryId { get; set; }

        [XmlArray("Match")]
        public ImportMatchDTO[] Matches { get; set; }
    }
}
