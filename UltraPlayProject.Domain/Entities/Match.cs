using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraPlayProject.Domain.Entities
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public MatchType MatchType { get; set; }

        [ForeignKey(nameof(Bet))]
        public int MatchId { get; set; }
        public Bet Bet { get; set; }
    }
}
