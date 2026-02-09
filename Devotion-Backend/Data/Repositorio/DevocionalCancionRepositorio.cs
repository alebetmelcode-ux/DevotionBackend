using Data.Interfaces.IRepositorio;
using Models.Entidades;

namespace Data.Repositorio
{
    public class DevocionalCancionRepositorio
        : Repositorio<DevocionalCancion>,
          IDevocionalCancionRepositorio
    {
        private readonly ApplicationDbContext _db;

        public DevocionalCancionRepositorio(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public void Actualizar(DevocionalCancion devocionalCancion)
        {
            _db.DevocionalCanciones.Update(devocionalCancion);
        }
    }
}
