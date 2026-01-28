using Microsoft.EntityFrameworkCore;
using rick_morty_backend.Models;
using System.Collections.Generic;
namespace rick_morty_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tabla Characters
        public DbSet<CharacterEntity> Characters { get; set; }
    }
}
