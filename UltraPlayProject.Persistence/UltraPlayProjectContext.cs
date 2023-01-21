using Microsoft.EntityFrameworkCore;
using UltraPlayProject.Domain.Entities;

namespace UltraPlayProject.Persistence
{
    public class UltraPlayProjectContext : DbContext
    {
        public UltraPlayProjectContext()
        {
        }

        public UltraPlayProjectContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Odd> Odds { get; set; }

       public DbSet<DatabaseLog> DatabaseLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=UltraPlayDb;Integrated Security=True;TrustServerCertificate=True");
            }
        }
    }
}
