using System.ComponentModel.DataAnnotations.Schema;
using UltraPlayProject.Domain.Enum;

namespace UltraPlayProject.Domain.Entities
{
    public class Match
    {
        public Match()
        {
            this.Bets = new List<Bet>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public MatchTypeEvent MatchType { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
