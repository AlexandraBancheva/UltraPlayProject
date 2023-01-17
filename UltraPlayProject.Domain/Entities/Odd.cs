using System.ComponentModel.DataAnnotations;

namespace UltraPlayProject.Domain.Entities
{
    public class Odd
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        // SpecialBetValue (SBV) is used in "Map Advantage", “Total Maps Played" and
        //others.It indicates the condition to win.For instance, the Total Maps Played
        //market can have odds of under 2.5 / over 2.5 maps played, where the SBV in that
        //case is 2.5.
        public double SpecialBetValue { get; set; }
    }
}