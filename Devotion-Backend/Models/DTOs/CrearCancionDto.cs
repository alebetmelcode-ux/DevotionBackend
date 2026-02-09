using System.ComponentModel.DataAnnotations;

public class CrearCancionDto
{
    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(60, MinimumLength = 5)]
    public string TituloCancion { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string TonoOriginal { get; set; } = string.Empty;

    [Required]
    public int IdCategoria { get; set; }

    [Required]
    public string Letra { get; set; } = string.Empty;

    // Flag solo de procesamiento, NO persistente
    public bool EsVistaMusico { get; set; } = false;
}
