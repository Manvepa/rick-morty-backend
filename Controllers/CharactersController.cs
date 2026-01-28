using Microsoft.AspNetCore.Mvc;
using rick_morty_backend.Services;
// Importamos nuestro servicio que habla con Rick & Morty
using rick_morty_backend.Services;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace rick_morty_backend.Controllers
{
    // Controlador de la Api
    [ApiController]

    // URL base del controlador
    [Route("api/characters")]
    public class CharactersController : ControllerBase
    {
        // Guardamos el servicio que va a buscar los personajes en internet
        private readonly RickMortyService _service;

        // Constructor que recibe el servicio via Inyección de Dependencias
        public CharactersController(RickMortyService service)
        {
            _service = service;
        }

        // Este método responde a peticiones GET
        [HttpGet]
        public async Task<IActionResult> GetCharacters(
            [FromQuery] string? name,
            [FromQuery] string? status,
            [FromQuery] string? species,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5)
        {
            // 1. Traemos todos los personajes desde el servicio
            var characters = await _service.GetCharacters();

            // 2. Filtrar por nombre si el usuario lo mandó
            if (!string.IsNullOrEmpty(name))
            {
                characters = characters
                    .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                    .ToList();
            }

            // 3. Filtrar por estado si el usuario lo mandó
            if (!string.IsNullOrEmpty(status))
            {
                characters = characters
                    .Where(c => c.Status.ToLower() == status.ToLower())
                    .ToList();
            }

            // 4. Filtrar por especie si el usuario lo mandó
            if (!string.IsNullOrEmpty(species))
            {
                characters = characters
                    .Where(c => c.Species.ToLower() == species.ToLower())
                    .ToList();
            }

            // 5. Calcular cuántos personajes saltar según la página
            int skip = (page - 1) * pageSize;

            // 6. Tomar solo los personajes de esta página
            var pagedCharacters = characters
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            // 7. Devolver resultado con información extra para el frontend
            return Ok(new
            {
                total = characters.Count,      // Total de personajes encontrados
                page = page,                  // Página actual
                pageSize = pageSize,         // Tamaño de página
                results = pagedCharacters    // Personajes de esta página
            });
        }

        // GET /api/characters/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            try
            {
                // Intentamos obtener el personaje desde el servicio
                var character = await _service.GetCharacterById(id);

                // Si todo sale bien, devolvemos el personaje
                return Ok(character);
            }
            catch
            {
                // Si ocurre cualquier error (ID no existe, API caída, etc)
                return NotFound(new
                {
                    message = "Personaje no encontrado"
                });
            }
        }

    }
}
