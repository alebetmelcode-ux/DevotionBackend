namespace Models.DTOs
{
    public class DevocionalCancionDetalleDto
    {
        public int CancionId { get; set; }
        public string TituloCancion { get; set; }
        public string Letra { get; set; }
        public string TonoOriginal { get; set; }
        public int PosicionCancion { get; set; }
    }
}
