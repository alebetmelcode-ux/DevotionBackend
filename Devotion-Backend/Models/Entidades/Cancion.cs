using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entidades
{
    public class Cancion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string TituloCancion { get; set; } = string.Empty;

        public string Letra { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string TonoOriginal { get; set; } = string.Empty;

        public int IdCategoria { get; set; }
        
        
        [NotMapped]
        public bool EsVistaMusico { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaActualizacion { get; set; }

        // Navegación
        public ICollection<DevocionalCancion> Devocionales { get; set; }
            = new List<DevocionalCancion>();
          
    }
}
