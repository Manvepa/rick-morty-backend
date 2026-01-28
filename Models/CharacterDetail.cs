namespace rick_morty_backend.Models
{
    public class CharacterDetail
    {
        // Id del personaje
        public int Id { get; set; }

        // Nombre del personaje
        public string Name { get; set; }

        // Estado (Alive, Dead, unknown)
        public string Status { get; set; }

        // Especie (Human, Alien, etc)
        public string Species { get; set; }

        // Imagen grande del personaje
        public string Image { get; set; }

        // Nombre de la ubicación actual
        public string Location { get; set; }

        // Lista de episodios donde aparece
        public List<string> Episodes { get; set; }
    }
}
