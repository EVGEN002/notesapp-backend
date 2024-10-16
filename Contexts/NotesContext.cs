using Microsoft.EntityFrameworkCore;
using TestApplication.Models;

namespace TestApplication.Contexts
{
    public class NotesContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=notes_app;Username=postgres;Password=1234;Command Timeout=300");
        }

        public DbSet<Note> notes { get; set; } = null!;
        public DbSet<Pause> pauses { get; set; } = null!;
    }
}
