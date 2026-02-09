using Data;
using Data.Interfaces.IRepositorio;
using Data.Repositorio;

public class DevocionalRepositorio
    : Repositorio<Devocional>, IDevocionalRepositorio
{
    private readonly ApplicationDbContext _db;

    public DevocionalRepositorio(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Actualizar(Devocional devocional)
    {
        _db.Devocionales.Update(devocional);
    }
}
