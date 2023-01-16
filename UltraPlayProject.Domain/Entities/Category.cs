using System.ComponentModel.DataAnnotations;

namespace UltraPlayProject.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
    }
}