using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CategoriaDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "la descripción debe ser Minimo 10  Máximo 60 caracteres")]
        public string DescripcionCategoria { get; set; }
    }
}
