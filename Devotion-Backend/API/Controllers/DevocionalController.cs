using BLL.Servicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{

    public class DevocionalController : BaseApiController
    {
        private readonly IDevocionalServicio _devocionalServicio;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ApiResponse _response;

        public DevocionalController(
            IDevocionalServicio devocionalServicio,
            IUnidadTrabajo unidadTrabajo)
        {
            _devocionalServicio = devocionalServicio;
            _unidadTrabajo = unidadTrabajo;
            _response = new ApiResponse();
        }

        // ============================
        // GET: api/Devocional
        // ============================
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _devocionalServicio.ObtenerTodos();
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        // ============================
        // GET: api/Devocional/{id}
        // ============================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        IsExitoso = false,
                        Mensaje = "Id inválido",
                        StatusCode = HttpStatusCode.BadRequest
                    });
                }

                var devocional = await _devocionalServicio.ObtenerPorId(id);

                if (devocional == null)
                {
                    return NotFound(new ApiResponse
                    {
                        IsExitoso = false,
                        Mensaje = "Devocional no encontrado",
                        StatusCode = HttpStatusCode.NotFound
                    });
                }

                _response.Resultado = devocional;
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        // ============================
        // POST: api/Devocional
        // ============================
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] DevocionalCrearDto modeloDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse
                    {
                        IsExitoso = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        Mensaje = "Datos inválidos",
                        Resultado = ModelState
                    });
                }

                var username = User.Identity?.Name;

                if (string.IsNullOrWhiteSpace(username))
                    return Unauthorized("Token inválido.");

                var usuario = await _unidadTrabajo.Usuario
                    .ObtenerPrimero(u => u.Username == username);

                if (usuario == null)
                    return Unauthorized("Usuario no existe.");

                var devocionalDto = new DevocionalDto
                {
                    NombreDevocional = modeloDto.NombreDevocional,
                    UsuarioId = usuario.Id
                };

                var resultado = await _devocionalServicio.Agregar(devocionalDto);

                _response.Resultado = resultado;
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.Created;

                return StatusCode(StatusCodes.Status201Created, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        // ============================
        // PUT: api/Devocional
        // ============================
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] DevocionalDto modeloDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse
                    {
                        IsExitoso = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        Mensaje = "Datos inválidos",
                        Resultado = ModelState
                    });
                }

                await _devocionalServicio.Actualizar(modeloDto);

                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        // ============================
        // DELETE: api/Devocional/{id}
        // ============================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Id inválido.");

                await _devocionalServicio.Remover(id);

                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        // ============================
        // POST: api/Devocional/agregar-canciones
        // ============================
        [HttpPost("agregar-canciones")]
        public async Task<IActionResult> AgregarCanciones(
            [FromBody] AgregarCancionesDevocionalDto dto)
        {
            try
            {
                if (dto.DevocionalId <= 0 ||
                    dto.CancionIds == null ||
                    dto.CancionIds.Count == 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        IsExitoso = false,
                        Mensaje = "Datos inválidos",
                        StatusCode = HttpStatusCode.BadRequest
                    });
                }

                await _devocionalServicio.AgregarCanciones(
                    dto.DevocionalId,
                    dto.CancionIds);

                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Mensaje = "Canciones agregadas correctamente";

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        // ============================
        // GET: api/Devocional/{id}/canciones
        // ============================
        [HttpGet("{id:int}/canciones")]
        public async Task<IActionResult> ObtenerCanciones(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        IsExitoso = false,
                        Mensaje = "Id inválido",
                        StatusCode = HttpStatusCode.BadRequest
                    });
                }

                var canciones = await _devocionalServicio.ObtenerCanciones(id);

                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Resultado = canciones;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        // ============================
        // DELETE: api/Devocional/{devocionalId}/canciones/{cancionId}
        // ============================
        [HttpDelete("{devocionalId:int}/canciones/{cancionId:int}")]
        public async Task<IActionResult> EliminarCancion(
            int devocionalId,
            int cancionId)
        {
            try
            {
                await _devocionalServicio.RemoverCancion(
                    devocionalId,
                    cancionId);

                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Mensaje = "Canción eliminada del devocional";

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }

        // ============================
        // PUT: api/Devocional/{id}/reordenar-canciones
        // ============================
        [HttpPut("{id:int}/reordenar-canciones")]
        public async Task<IActionResult> ReordenarCanciones(
            int id,
            [FromBody] List<DevocionalCancionDto> canciones)
        {
            try
            {
                if (id <= 0 ||
                    canciones == null ||
                    canciones.Count == 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        IsExitoso = false,
                        Mensaje = "Datos inválidos",
                        StatusCode = HttpStatusCode.BadRequest
                    });
                }

                await _devocionalServicio.ReordenarCanciones(id, canciones);

                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Mensaje = "Orden actualizado correctamente";

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }
    }
}
