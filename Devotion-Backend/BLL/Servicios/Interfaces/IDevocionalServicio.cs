using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Servicios.Interfaces
{
    public interface IDevocionalServicio
    {
        // ============================
        // OBTENER DEVOCIONALES
        // ============================
        Task<IEnumerable<DevocionalDto>> ObtenerTodos();

        // ============================
        // CREAR DEVOCIONAL
        // ============================
        Task<DevocionalDto> Agregar(DevocionalDto modeloDto);

        // ============================
        // EDITAR DEVOCIONAL
        // ============================
        Task Actualizar(DevocionalDto modeloDto);

        // ============================
        // ELIMINAR DEVOCIONAL
        // ============================
        Task Remover(int id);

        // ============================
        // AGREGAR CANCIONES A DEVOCIONAL
        // ============================
        Task AgregarCanciones(
            int devocionalId,
            List<int> cancionIds);

        // ============================
        // OBTENER CANCIONES
        // ============================
        Task<List<DevocionalCancionDetalleDto>> ObtenerCanciones(
            int devocionalId);

        // ============================
        // OBTENER DEVOCIONAL POR ID
        // ============================
        Task<DevocionalDto?> ObtenerPorId(int id);

        // ============================
        // REORDENAR CANCIONES
        // ============================
        Task ReordenarCanciones(
            int devocionalId,
            List<DevocionalCancionDto> canciones);

        // ============================
        // ELIMINAR CANCIÓN DEL DEVOCIONAL
        // ============================
        Task RemoverCancion(
            int devocionalId,
            int cancionId);
    }
}
