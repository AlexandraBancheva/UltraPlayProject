using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UltraPlayProject.Domain.Entities
{
    public class Sport
    {
        public Sport()
        {
            this.Events = new List<Event>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
