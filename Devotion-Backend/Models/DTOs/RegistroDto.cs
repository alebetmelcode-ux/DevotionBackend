using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class RegistroDto
    {
        [Required(ErrorMessage = "Username es requerido")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password es requerido")]
        [StringLength(
            8,
            MinimumLength = 4,
            ErrorMessage = "El password debe tener mínimo 4 y máximo 8 caracteres"
        )]
        public string Password { get; set; } = string.Empty;
    }
}