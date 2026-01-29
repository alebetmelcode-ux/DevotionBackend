using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configuraciones
{
    public class CancionConfiguracion : IEntityTypeConfiguration<Cancion>
    {
        public void Configure(EntityTypeBuilder<Cancion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TituloCancion)
                   .IsRequired()
                   .HasMaxLength(60);

            builder.Property(x => x.TonoOriginal);

            builder.HasMany(d => d.Devocionales)
                   .WithOne(dc => dc.Cancion)
                   .HasForeignKey(dc => dc.CancionId);
        }
    }
}
