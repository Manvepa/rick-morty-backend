using System.Text.Json;
using rick_morty_backend.Data;
using rick_morty_backend.Models;

namespace rick_morty_backend.Services
{
    public class CharacterSeedService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public CharacterSeedService(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task SeedIfEmptyAsync()
        {
            Console.WriteLine(">>> SeedIfEmptyAsync START");

            // Esperar a que MySQL esté disponible
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    _context.Database.CanConnect();
                    break;
                }
                catch
                {
                    Console.WriteLine(">>> Waiting for MySQL...");
                    await Task.Delay(2000);
                }


                if (_context.Characters.Any())
                {
                    Console.WriteLine(">>> Characters table NOT empty, skipping seed");
                    return;
                }

                Console.WriteLine(">>> Characters table EMPTY, seeding data");

                var response = await _httpClient.GetStringAsync(
                    "https://rickandmortyapi.com/api/character?page=1"
                );

                using var json = JsonDocument.Parse(response);
                var results = json.RootElement.GetProperty("results");

                foreach (var item in results.EnumerateArray())
                {
                    var entity = new CharacterEntity
                    {
                        Name = item.GetProperty("name").GetString()!,
                        Status = item.GetProperty("status").GetString()!,
                        Species = item.GetProperty("species").GetString()!,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.Characters.Add(entity);
                }

                await _context.SaveChangesAsync();
                Console.WriteLine(">>> Seed COMPLETED, data saved");
            }
        }
    }
}