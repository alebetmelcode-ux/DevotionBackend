using AutoMapper;
using BLL.Servicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Servicios
{
    public class CancionServicio : ICancionServicio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public CancionServicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        // =========================
        // CREAR INDIVIDUAL
        // =========================
        public async Task<CancionDto> Agregar(CrearCancionDto modeloDto)
        {
            try
            {
                var cancion = new Cancion
                {
                    TituloCancion = modeloDto.TituloCancion,
                    TonoOriginal = modeloDto.TonoOriginal,
                    IdCategoria = modeloDto.IdCategoria,
                    Letra = modeloDto.Letra,
                    FechaCreacion = DateTime.UtcNow,
                    FechaActualizacion = DateTime.UtcNow
                };

                await _unidadTrabajo.Cancion.Agregar(cancion);
                await _unidadTrabajo.Guardar();

                if (cancion.Id == 0)
                    throw new TaskCanceledException("La canción no se pudo crear");

                return _mapper.Map<CancionDto>(cancion);
            }
            catch
            {
                throw;
            }
        }

        // =========================
        // CREAR MASIVO
        // =========================
        public async Task AgregarMasivo(List<CrearCancionDto> cancionesDto)
        {
            try
            {
                if (cancionesDto == null || !cancionesDto.Any())
                    throw new TaskCanceledException("La lista de canciones está vacía");

                var canciones = cancionesDto.Select(dto => new Cancion
                {
                    TituloCancion = dto.TituloCancion,
                    TonoOriginal = dto.TonoOriginal,
                    IdCategoria = dto.IdCategoria,
                    Letra = dto.Letra,
                    FechaCreacion = DateTime.UtcNow,
                    FechaActualizacion = DateTime.UtcNow
                }).ToList();

                await _unidadTrabajo.Cancion.AgregarRango(canciones);
                await _unidadTrabajo.Guardar();
            }
            catch
            {
                throw;
            }
        }

        // =========================
        // ACTUALIZAR
        // =========================
        public async Task Actualizar(CancionDto modeloDto)
        {
            try
            {
                var cancionDb = await _unidadTrabajo.Cancion.ObtenerPrimero(
                    c => c.Id == modeloDto.Id);

                if (cancionDb == null)
                    throw new TaskCanceledException("La canción no existe");

                cancionDb.TituloCancion = modeloDto.TituloCancion;
                cancionDb.TonoOriginal = modeloDto.TonoOriginal;
                cancionDb.IdCategoria = modeloDto.IdCategoria;
                cancionDb.Letra = modeloDto.Letra;
                cancionDb.FechaActualizacion = DateTime.UtcNow;

                _unidadTrabajo.Cancion.Actualizar(cancionDb);
                await _unidadTrabajo.Guardar();
            }
            catch
            {
                throw;
            }
        }

        // =========================
        // OBTENER TODOS
        // =========================
        public async Task<IEnumerable<CancionDto>> ObtenerTodos()
        {
            try
            {
                var lista = await _unidadTrabajo.Cancion.ObtenerTodos(
                    orderBy: c => c.OrderBy(e => e.TituloCancion));

                return _mapper.Map<IEnumerable<CancionDto>>(lista);
            }
            catch
            {
                throw;
            }
        }

        // =========================
        // ELIMINAR INDIVIDUAL
        // =========================
        public async Task Remover(int id)
        {
            try
            {
                var cancionDb = await _unidadTrabajo.Cancion.ObtenerPrimero(
                    c => c.Id == id);

                if (cancionDb == null)
                    throw new TaskCanceledException("La canción no existe");

                _unidadTrabajo.Cancion.Remover(cancionDb);
                await _unidadTrabajo.Guardar();
            }
            catch
            {
                throw;
            }
        }

        // =========================
        // ELIMINAR MASIVO ✅ NUEVO
        // =========================
        public async Task RemoverMasivo(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new TaskCanceledException("La lista de IDs está vacía");

                var canciones = await _unidadTrabajo.Cancion.ObtenerTodos(
                    c => ids.Contains(c.Id));

                if (canciones == null || !canciones.Any())
                    throw new TaskCanceledException("No se encontraron canciones para eliminar");

                _unidadTrabajo.Cancion.RemoverRango(canciones);
                await _unidadTrabajo.Guardar();
            }
            catch
            {
                throw;
            }
        }
    }
}
