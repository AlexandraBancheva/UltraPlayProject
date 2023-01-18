using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraPlayProject.Domain.Entities
{
    public class Event
    {
        public Event()
        {
            this.Matches = new List<Match>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
