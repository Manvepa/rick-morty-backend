using Microsoft.AspNetCore.Mvc;

namespace rick_morty_backend.Controllers
{
    // Este controlador sirve solo para verificar que la API está viva
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        // Responde a: GET /api/health
        [HttpGet]
        public IActionResult Health()
        {
            // Devolvemos un mensaje simple
            return Ok(new
            {
                status = "UP",
                message = "Backend funcionando correctamente"
            });
        }
    }
}
