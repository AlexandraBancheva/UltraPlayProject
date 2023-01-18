using System.Xml.Serialization;
using UltraPlayProject.Domain.Entities;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Sport")]
    public class ImportSportDTO
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("ID")]
        public int Id { get; set; }

        [XmlArray("Event")]
        public ImportEventDTO[] Events { get; set; }
    }
}
