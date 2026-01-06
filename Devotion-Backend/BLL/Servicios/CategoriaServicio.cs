using AutoMapper;
using BLL.Servicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios
{
    public class CategoriaServicio : ICategoriaServicio

    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public CategoriaServicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        public async Task<CategoriaDto> Agregar(CategoriaDto modeloDto)
        {
            try
            {
                Categoria categoria = new Categoria
                {
                    DescripcionCategoria = modeloDto.DescripcionCategoria,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                };
                await _unidadTrabajo.Categoria.Agregar(categoria);
                await _unidadTrabajo.Guardar();
                if(categoria.Id == 0)
                    throw new TaskCanceledException("la categoría no se puedo crear");
                return _mapper.Map<CategoriaDto>(categoria);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public async Task Actualizar(CategoriaDto modeloDto)
        {
            try
            {
                var categoriaDb = await _unidadTrabajo.Categoria.ObtenerPrimero(c => c.Id == modeloDto.Id);
                if (categoriaDb == null)
                    throw new TaskCanceledException("la categoría no existe");

                categoriaDb.DescripcionCategoria = modeloDto.DescripcionCategoria;
                _unidadTrabajo.Categoria.Actualizar(categoriaDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {

                throw;
            }

           
        }

        public async Task Remover(int id)
        {
            try
            {
                var categoriaDb = await _unidadTrabajo.Categoria.ObtenerPrimero(c => c.Id == id);
                if (categoriaDb == null)
                    throw new TaskCanceledException("la categoría no existe");
                _unidadTrabajo.Categoria.Remover(categoriaDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<IEnumerable<CategoriaDto>> ObtenerTodos()
        {
            try
            {
                var lista = await _unidadTrabajo.Categoria.ObtenerTodos(
                    orderBy: c => c.OrderBy(e => e.DescripcionCategoria));
                return _mapper.Map<IEnumerable<CategoriaDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        
    }
}
