using System.Text.Json;
using rick_morty_backend.Models;

namespace rick_morty_backend.Services
{
    //Clase caracter
    public class RickMortyService
    {
        private readonly HttpClient _http;

        // Constructor
        public RickMortyService(HttpClient http)
        {
            _http = http;
        }

        // Este método va a buscar personajes reales en internet
        public async Task<List<Character>> GetCharacters()
        {
            // Llamamos a la API pública de Rick & Morty
            var response = await _http.GetAsync("https://rickandmortyapi.com/api/character");
            // Si algo sale mal (error 404, 500, etc), lanza un error
            response.EnsureSuccessStatusCode();

            // Leemos la respuesta como texto (JSON)
            var json = await response.Content.ReadAsStringAsync();
            // Convertimos el texto JSON
            using var doc = JsonDocument.Parse(json);

            // Sacamos la parte "results" que es donde vienen los personajes
            var results = doc.RootElement.GetProperty("results");

            // Creamos una lista vacía para guardar los personajes
            var characters = new List<Character>();

            // Recorremos cada personaje que vino de la API
            foreach (var item in results.EnumerateArray())
            {
                // Creamos un personaje con solo los datos que nos interesan
                characters.Add(new Character
                {
                    Id = item.GetProperty("id").GetInt32(),
                    Name = item.GetProperty("name").GetString(),
                    Status = item.GetProperty("status").GetString(),
                    Species = item.GetProperty("species").GetString(),
                    Image = item.GetProperty("image").GetString()
                });
            }

            // Devolvemos la lista de personajes ya limpia
            return characters;
        }

        // Este método obtiene el detalle completo de un personaje por ID
        public async Task<CharacterDetail> GetCharacterById(int id)
        {
            // Llamamos a la API externa usando el ID
            var response = await _http.GetAsync(
                $"https://rickandmortyapi.com/api/character/{id}"
            );

            // Si el ID no existe o hay error, lanza excepción
            response.EnsureSuccessStatusCode();

            // Leemos el JSON como texto
            var json = await response.Content.ReadAsStringAsync();

            // Convertimos el JSON a algo entendible por C#
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            // Creamos el objeto de detalle
            var character = new CharacterDetail
            {
                Id = root.GetProperty("id").GetInt32(),
                Name = root.GetProperty("name").GetString(),
                Status = root.GetProperty("status").GetString(),
                Species = root.GetProperty("species").GetString(),
                Image = root.GetProperty("image").GetString(),

                // Ubicación es un objeto, sacamos solo el nombre
                Location = root.GetProperty("location").GetProperty("name").GetString(),

                // Episodios es una lista de URLs
                Episodes = root.GetProperty("episode")
                               .EnumerateArray()
                               .Select(e => e.GetString())
                               .ToList()
            };

            return character;
        }

    }
}
