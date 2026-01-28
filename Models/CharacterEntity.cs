using System;

namespace rick_morty_backend.Models
{
    public class CharacterEntity
    {
        // Id del personaje (API)
        public int Id { get; set; }
        // Nombre
        public string Name { get; set; }
        // Estado
        public string Status { get; set; }
        // Especie
        public string Species { get; set; }
        // Fecha de guardado
        public DateTime CreatedAt { get; set; } 
    }
}
