using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CancionDto
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string TituloCancion { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string TonoOriginal { get; set; } = string.Empty;

        [Required]
        public int IdCategoria { get; set; }

        [Required]
        public string Letra { get; set; } = string.Empty;
    }
}
