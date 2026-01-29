using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UsuarioController(
        ApplicationDbContext db,
        ITokenServicio tokenServicio
    ) : BaseApiController
    {
        private readonly ApplicationDbContext _db = db;
        private readonly ITokenServicio _tokenServicio = tokenServicio;

        // GET: api/Usuario
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _db.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuario/1
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // POST: api/Usuario/registro
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.Username))
                return BadRequest("Username ya está registrado");

            using var hmac = new HMACSHA512();

            var usuario = new Usuario
            {
                Username = registroDto.Username,
                PasswordHash = hmac.ComputeHash(
                    Encoding.UTF8.GetBytes(registroDto.Password)
                ),
                PasswordSalt = hmac.Key
            };

            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            return new UsuarioDto
            {
                Username = usuario.Username,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Datos inválidos");

            var usuario = await _db.Usuarios
                .SingleOrDefaultAsync(x => x.Username == loginDto.Username);

            if (usuario == null)
                return Unauthorized("Usuario no válido");

            if (usuario.PasswordSalt == null || usuario.PasswordHash == null)
                return Unauthorized("Credenciales inválidas");

            using var hmac = new HMACSHA512(usuario.PasswordSalt);
            var computedHash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.PasswordHash[i])
                    return Unauthorized("Password no válido");
            }

            return new UsuarioDto
            {
                Username = usuario.Username,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }



        private async Task<bool> UsuarioExiste(string username)
        {
            return await _db.Usuarios.AnyAsync(x =>
                string.Equals(
                    x.Username,
                    username,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }
    }
}
