using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraPlayProject.Domain.Entities
{
    public class Bet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsLive { get; set; }

        [ForeignKey(nameof(Odd))]
        public int OddId { get; set; }
        public Odd Odd { get; set; }
    }
}
