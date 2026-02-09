using Models.Entidades;
using System.ComponentModel.DataAnnotations;

public class Devocional
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del devocional es obligatorio")]
    [MaxLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres")]
    public string NombreDevocional { get; set; } = string.Empty;

    public DateTime Fecha { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public ICollection<DevocionalCancion> DevocionalCanciones { get; set; }
        = new List<DevocionalCancion>();
}
