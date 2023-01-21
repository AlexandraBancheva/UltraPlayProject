using System.Xml.Serialization;

namespace UltraPlayProject.Application.DTOs.ImportDTOs
{
    [XmlType("Event")]
    public class ImportEventDTO
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlAttribute("IsLive")]
        public bool IsLive { get; set; }

        [XmlAttribute("CategoryID")]
        public int CategoryID { get; set; }

        [XmlElement("Match")]
        public ImportMatchDTO[] Matches { get; set; }
    }
}
