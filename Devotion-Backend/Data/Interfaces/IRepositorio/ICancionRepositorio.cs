using Models.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepositorio
{
    public interface ICancionRepositorio : IRepositorioGenerico<Cancion>
    {
        void Actualizar(Cancion cancion);

        Task AgregarRango(IEnumerable<Cancion> canciones);

        // ============================
        // ELIMINAR MASIVO ✅ NUEVO
        // ============================
        void RemoverRango(IEnumerable<Cancion> canciones);
    }
}
