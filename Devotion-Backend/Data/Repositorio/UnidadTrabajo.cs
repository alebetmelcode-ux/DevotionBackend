using Data.Interfaces;
using Data.Interfaces.IRepositorio;
using Data.Repositorio;

namespace Data.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ICancionRepositorio Cancion { get; private set; }
        public ICategoriaRepositorio Categoria { get; private set; }
        public IDevocionalRepositorio Devocional { get; private set; }
        public IDevocionalCancionRepositorio DevocionalCancion { get; private set; }
        public IUsuarioRepositorio Usuario { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;

            Cancion = new CancionRepositorio(db);
            Categoria = new CategoriaRepositorio(db);
            Devocional = new DevocionalRepositorio(db);
            DevocionalCancion = new DevocionalCancionRepositorio(db);
            Usuario = new UsuarioRepositorio(db);
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
