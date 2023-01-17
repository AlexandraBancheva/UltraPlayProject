using System.Xml.Serialization;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Odd")]
    public class ImportOddDTO
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("ID")]
        public int Id { get; set; }

        [XmlElement("Value")]
        public double Value { get; set; }

        [XmlElement("SpecialBetValue")]
        public double? SpecialBetValue { get; set; }
    }
}
