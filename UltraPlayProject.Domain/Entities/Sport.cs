namespace UltraPlayProject.Domain.Entities
{
    public class Sport
    {
        public Sport()
        {
            this.Events = new List<Event>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
