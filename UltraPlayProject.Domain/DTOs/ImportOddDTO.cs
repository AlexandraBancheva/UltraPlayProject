using System.Xml.Serialization;

namespace UltraPlayProject.Domain.DTOs
{
    [XmlType("Odd")]
    public class ImportOddDTO
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlAttribute("Value")]
        public double Value { get; set; }

        [XmlElement(IsNullable = true)]
        public double? SpecialBetValue { get; set; }
    }
}
