using System;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        ICancionRepositorio Cancion { get; }
        ICategoriaRepositorio Categoria { get; }
        IDevocionalRepositorio Devocional { get; }
        IDevocionalCancionRepositorio DevocionalCancion { get; }
        IUsuarioRepositorio Usuario { get; }

        Task Guardar();
    }
}
