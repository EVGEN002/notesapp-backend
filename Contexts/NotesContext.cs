using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;

namespace NotesAPI.Contexts
{
    public class NotesContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=notes_db;Username=postgres;Password=1234;Command Timeout=300");
        }

        public DbSet<Note> notes { get; set; } = null!;
        public DbSet<Pause> pauses { get; set; } = null!;
    }
}
