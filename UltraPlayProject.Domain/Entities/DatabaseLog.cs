using System.ComponentModel.DataAnnotations;

namespace UltraPlayProject.Domain.Entities
{
    public class DatabaseLog
    {
        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }
    }
}
