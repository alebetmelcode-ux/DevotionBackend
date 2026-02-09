using BLL.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    public class CancionController : BaseApiController
    {
        private readonly ICancionServicio _cancionServicio;
        private ApiResponse _response;

        public CancionController(ICancionServicio cancionServicio)
        {
            _cancionServicio = cancionServicio;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _cancionServicio.ObtenerTodos();
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        // ============================
        // CREAR CANCIÓN (SINGLE)
        // ============================
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearCancionDto modeloDto)
        {
            try
            {
                await _cancionServicio.Agregar(modeloDto);

                return StatusCode(StatusCodes.Status201Created, new ApiResponse
                {
                    IsExitoso = true,
                    StatusCode = HttpStatusCode.Created,
                    Mensaje = "La canción fue creada correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    IsExitoso = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Mensaje = ex.Message
                });
            }
        }

        // ============================
        // EDITAR CANCIÓN
        // ============================
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] CancionDto modeloDto)
        {
            try
            {
                await _cancionServicio.Actualizar(modeloDto);
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        // ============================
        // ELIMINAR (SINGLE)
        // ============================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _cancionServicio.Remover(id);
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        // ============================
        // ELIMINAR MASIVO ✅ NUEVO
        // ============================
        [HttpDelete("eliminar-masivo")]
        public async Task<IActionResult> EliminarMasivo([FromBody] List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                {
                    return BadRequest(new ApiResponse
                    {
                        IsExitoso = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        Mensaje = "La lista de IDs está vacía"
                    });
                }

                await _cancionServicio.RemoverMasivo(ids);

                return Ok(new ApiResponse
                {
                    IsExitoso = true,
                    StatusCode = HttpStatusCode.OK,
                    Mensaje = $"Se eliminaron {ids.Count} canciones correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    IsExitoso = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Mensaje = ex.Message
                });
            }
        }

        // ============================
        // CARGA MASIVA
        // ============================
        [HttpPost("carga-masiva")]
        public async Task<IActionResult> CargaMasiva(
            [FromBody] List<CrearCancionDto> cancionesDto)
        {
            try
            {
                if (cancionesDto == null || !cancionesDto.Any())
                {
                    return BadRequest(new ApiResponse
                    {
                        IsExitoso = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        Mensaje = "La lista de canciones está vacía"
                    });
                }

                await _cancionServicio.AgregarMasivo(cancionesDto);

                return StatusCode(StatusCodes.Status201Created, new ApiResponse
                {
                    IsExitoso = true,
                    StatusCode = HttpStatusCode.Created,
                    Mensaje = $"Se cargaron {cancionesDto.Count} canciones correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    IsExitoso = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Mensaje = ex.Message
                });
            }
        }
    }
}
