using Models.Entidades;

namespace Data.Interfaces.IRepositorio
{
    public interface IDevocionalRepositorio
        : IRepositorioGenerico<Devocional>
    {
        void Actualizar(Devocional devocional);
    }
}
