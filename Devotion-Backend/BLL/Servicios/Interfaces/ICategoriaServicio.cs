using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios.Interfaces
{
    public interface ICategoriaServicio
    {
        Task<IEnumerable<CategoriaDto>> ObtenerTodos();

        Task<CategoriaDto> Agregar(CategoriaDto modeloDto);

        Task Actualizar(CategoriaDto modeloDto);

        Task Remover(int id);
    }
}
