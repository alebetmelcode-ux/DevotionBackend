using System.Collections.Generic;

namespace Models.DTOs
{
    public class DevocionalDto
    {
        public int Id { get; set; }
        public string NombreDevocional { get; set; }
        public int UsuarioId { get; set; }
        public List<DevocionalCancionDto> Canciones { get; set; } = new();
    }
}