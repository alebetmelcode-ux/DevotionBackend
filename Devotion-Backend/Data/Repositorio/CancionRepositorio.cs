using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class CancionRepositorio : Repositorio<Cancion>, ICancionRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CancionRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // =========================
        // ACTUALIZAR
        // =========================
        public void Actualizar(Cancion cancion)
        {
            var cancionDb = _db.Canciones.FirstOrDefault(c => c.Id == cancion.Id);
            if (cancionDb != null)
            {
                cancionDb.TituloCancion = cancion.TituloCancion;
                cancionDb.TonoOriginal = cancion.TonoOriginal;
                cancionDb.IdCategoria = cancion.IdCategoria;
                cancionDb.Letra = cancion.Letra;
                cancionDb.FechaActualizacion = DateTime.UtcNow;

                _db.Canciones.Update(cancionDb);
                // ❌ NO SaveChanges aquí
            }
        }

        // =========================
        // AGREGAR RANGO (MASIVO)
        // =========================
        public async Task AgregarRango(IEnumerable<Cancion> canciones)
        {
            await _db.Canciones.AddRangeAsync(canciones);
            // ❌ NO SaveChanges aquí
        }

        // =========================
        // ELIMINAR RANGO (MASIVO) ✅ NUEVO
        // =========================
        public void RemoverRango(IEnumerable<Cancion> canciones)
        {
            _db.Canciones.RemoveRange(canciones);
            // ❌ NO SaveChanges aquí
        }
    }
}
