using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Servicios.Interfaces
{
    public interface ICancionServicio
    {
        Task<IEnumerable<CancionDto>> ObtenerTodos();

        Task<CancionDto> Agregar(CrearCancionDto dto);

        Task AgregarMasivo(List<CrearCancionDto> dtos);

        Task Actualizar(CancionDto dto);

        Task Remover(int id);

        // ============================
        // ELIMINAR MASIVO ✅ NUEVO
        // ============================
        Task RemoverMasivo(List<int> ids);
    }
}
