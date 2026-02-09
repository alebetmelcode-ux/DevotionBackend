using Data.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _db;
         
        public UsuarioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Usuario usuario)
        {
            _db.Usuarios.Update(usuario);
        }
    }
}
