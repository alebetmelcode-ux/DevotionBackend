using System.ComponentModel.DataAnnotations;

public class DevocionalCrearDto
{
    [Required(ErrorMessage = "El nombre del devocional es obligatorio")]
    [MaxLength(150)]
    public string NombreDevocional { get; set; } = string.Empty;
}

