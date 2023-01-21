using System.Xml.Serialization;

namespace UltraPlayProject.Application.DTOs.ImportDTOs
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

        [XmlAttribute("SpecialBetValue")]
        public double SpecialBetValue { get; set; }
    }
}
