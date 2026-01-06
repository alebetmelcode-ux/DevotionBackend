using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio

    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Categoria categoria)
        {
            var categoriaDb = _db.Categorias.FirstOrDefault(c => c.Id == categoria.Id);
            if (categoriaDb != null)
            {
                categoriaDb.DescripcionCategoria = categoria.DescripcionCategoria;
                categoriaDb.FechaActualizacion = DateTime.Now;
                _db.SaveChanges();
            }
        }
    }
}
