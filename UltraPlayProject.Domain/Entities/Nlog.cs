using System.ComponentModel.DataAnnotations;

namespace UltraPlayProject.Domain.Entities
{
    public class Nlog
    {
        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }
    }
}
