using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraPlayProject.Domain.Entities
{
    public class Match
    {
        public Match()
        {
            this.Bets = new List<Bet>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public MatchType MatchType { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
