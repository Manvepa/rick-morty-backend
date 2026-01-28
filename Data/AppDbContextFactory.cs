using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace rick_morty_backend.Data
{
    // Esta clase SOLO se usa cuando ejecutamos comandos como:
    // dotnet ef migrations add
    // dotnet ef database update
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Creamos las opciones del DbContext manualmente
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Connection string directa para MySQL en Docker
            var connectionString =
                "server=localhost;port=3306;database=rickmortydb;user=root;password=root";

            // Le decimos a EF que use MySQL con Pomelo
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );

            // Devolvemos el contexto listo para migraciones
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
