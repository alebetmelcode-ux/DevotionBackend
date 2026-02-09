using Models.Entidades;

namespace Data.Interfaces.IRepositorio
{
    public interface IUsuarioRepositorio
        : IRepositorioGenerico<Usuario>
    {
        void Actualizar(Usuario usuario);
    }
}
