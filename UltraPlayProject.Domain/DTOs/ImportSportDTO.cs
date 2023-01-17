using System.Xml.Serialization;
using UltraPlayProject.Domain.Entities;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Sport")]
    public class ImportSportDTO
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("ID")]
        public int Id { get; set; }

        [XmlArray("Event")]
        public ImportEventDTO[] Events { get; set; }
    }
}
