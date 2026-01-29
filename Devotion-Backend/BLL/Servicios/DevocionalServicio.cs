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
    public class DevocionalServicio : IDevocionalServicio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public DevocionalServicio(
            IUnidadTrabajo unidadTrabajo,
            IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        // ============================
        // OBTENER TODOS
        // ============================
        public async Task<IEnumerable<DevocionalDto>> ObtenerTodos()
        {
            var lista = await _unidadTrabajo.Devocional.ObtenerTodos(
                incluirPropiedades: "DevocionalCanciones.Cancion",
                orderBy: q => q.OrderBy(d => d.NombreDevocional));

            return _mapper.Map<IEnumerable<DevocionalDto>>(lista);
        }

        // ============================
        // CREAR DEVOCIONAL
        // ============================
        public async Task<DevocionalDto> Agregar(DevocionalDto modeloDto)
        {
            var devocional = new Devocional
            {
                NombreDevocional = modeloDto.NombreDevocional,
                UsuarioId = modeloDto.UsuarioId,
                Fecha = DateTime.UtcNow
            };

            await _unidadTrabajo.Devocional.Agregar(devocional);
            await _unidadTrabajo.Guardar();

            var devocionalDb = await _unidadTrabajo.Devocional.ObtenerPrimero(
                d => d.Id == devocional.Id,
                incluirPropiedades: "DevocionalCanciones.Cancion");

            return _mapper.Map<DevocionalDto>(devocionalDb);
        }

        // ============================
        // EDITAR DEVOCIONAL
        // ============================
        public async Task Actualizar(DevocionalDto modeloDto)
        {
            var devocionalDb = await _unidadTrabajo.Devocional
                .ObtenerPrimero(d => d.Id == modeloDto.Id);

            if (devocionalDb == null)
                throw new TaskCanceledException("El devocional no existe");

            devocionalDb.NombreDevocional = modeloDto.NombreDevocional;

            _unidadTrabajo.Devocional.Actualizar(devocionalDb);
            await _unidadTrabajo.Guardar();
        }

        // ============================
        // ELIMINAR DEVOCIONAL
        // ============================
        public async Task Remover(int id)
        {
            var devocionalDb = await _unidadTrabajo.Devocional
                .ObtenerPrimero(d => d.Id == id);

            if (devocionalDb == null)
                throw new TaskCanceledException("El devocional no existe");

            _unidadTrabajo.Devocional.Remover(devocionalDb);
            await _unidadTrabajo.Guardar();
        }

        // ============================
        // AGREGAR CANCIONES (CORRECTO)
        // ============================
        public async Task AgregarCanciones(
            int devocionalId,
            List<int> cancionIds)
        {
            if (cancionIds == null || !cancionIds.Any())
                return;

            var devocional = await _unidadTrabajo.Devocional
                .ObtenerPrimero(d => d.Id == devocionalId);

            if (devocional == null)
                throw new TaskCanceledException("Devocional no existe");

            var relacionesActuales = await _unidadTrabajo.DevocionalCancion
                .ObtenerTodos(dc => dc.DevocionalId == devocionalId);

            int posicionActual = relacionesActuales.Any()
                ? relacionesActuales.Max(x => x.PosicionCancion)
                : 0;

            foreach (var cancionId in cancionIds.Distinct())
            {
                // Evitar duplicados
                if (relacionesActuales.Any(dc => dc.CancionId == cancionId))
                    continue;

                posicionActual++;

                await _unidadTrabajo.DevocionalCancion.Agregar(
                    new DevocionalCancion
                    {
                        DevocionalId = devocionalId,
                        CancionId = cancionId,
                        PosicionCancion = posicionActual
                    });
            }

            await _unidadTrabajo.Guardar();
        }

        // ============================
        // OBTENER DEVOCIONAL POR ID
        // ============================
        public async Task<DevocionalDto?> ObtenerPorId(int id)
        {
            var devocional = await _unidadTrabajo.Devocional.ObtenerPrimero(
                d => d.Id == id,
                incluirPropiedades: "DevocionalCanciones.Cancion"
            );

            if (devocional == null)
                return null;

            return _mapper.Map<DevocionalDto>(devocional);
        }

        // ============================
        // OBTENER CANCIONES DEL DEVOCIONAL
        // ============================
        public async Task<List<DevocionalCancionDetalleDto>> ObtenerCanciones(int devocionalId)
        {
            var relaciones = await _unidadTrabajo.DevocionalCancion
                .ObtenerTodos(
                    dc => dc.DevocionalId == devocionalId,
                    incluirPropiedades: "Cancion");

            return relaciones
                .OrderBy(dc => dc.PosicionCancion)
                .Select(dc => new DevocionalCancionDetalleDto
                {
                    CancionId = dc.CancionId,
                    TituloCancion = dc.Cancion.TituloCancion,
                    Letra = dc.Cancion.Letra,
                    TonoOriginal = dc.Cancion.TonoOriginal,
                    PosicionCancion = dc.PosicionCancion
                })
                .ToList();
        }





        // ============================
        // REORDENAR CANCIONES
        // ============================
        public async Task ReordenarCanciones(
            int devocionalId,
            List<DevocionalCancionDto> canciones)
        {
            var relaciones = await _unidadTrabajo.DevocionalCancion
                .ObtenerTodos(dc => dc.DevocionalId == devocionalId);

            foreach (var item in canciones)
            {
                var relacion = relaciones
                    .FirstOrDefault(dc => dc.CancionId == item.CancionId);

                if (relacion != null)
                {
                    relacion.PosicionCancion = item.PosicionCancion;
                    _unidadTrabajo.DevocionalCancion.Actualizar(relacion);
                }
            }

            await _unidadTrabajo.Guardar();
        }

        // ============================
        // ELIMINAR CANCIÓN
        // ============================
        public async Task RemoverCancion(
            int devocionalId,
            int cancionId)
        {
            var relacion = await _unidadTrabajo.DevocionalCancion
                .ObtenerPrimero(dc =>
                    dc.DevocionalId == devocionalId &&
                    dc.CancionId == cancionId);

            if (relacion == null)
                throw new TaskCanceledException("La canción no existe en el devocional");

            _unidadTrabajo.DevocionalCancion.Remover(relacion);
            await _unidadTrabajo.Guardar();
        }
    }
}
