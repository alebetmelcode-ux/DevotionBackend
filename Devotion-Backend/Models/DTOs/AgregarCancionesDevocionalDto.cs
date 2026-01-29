namespace Models.DTOs
{
    public class AgregarCancionesDevocionalDto
    {
        public int DevocionalId { get; set; }
        public List<int> CancionIds { get; set; } = new();
    }
}
