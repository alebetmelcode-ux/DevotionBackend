using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class DevocionalCancion
    {
        public int Id { get; set; }
        public int DevocionalId { get; set; }
        public Devocional Devocional { get; set; }
        public int CancionId { get; set; }
        public Cancion Cancion { get; set; }
        public int PosicionCancion { get; set; }
      
    }
}
