using Models.Entidades;

namespace Data.Interfaces.IRepositorio
{
    public interface IDevocionalCancionRepositorio
        : IRepositorioGenerico<DevocionalCancion>
    {
        void Actualizar(DevocionalCancion devocionalCancion);
    }
}
