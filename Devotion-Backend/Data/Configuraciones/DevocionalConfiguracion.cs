using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;

namespace Data.Configuraciones
{
    public class DevocionalConfiguracion : IEntityTypeConfiguration<Devocional>
    {
        public void Configure(EntityTypeBuilder<Devocional> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.NombreDevocional)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(d => d.DevocionalCanciones)
                   .WithOne(dc => dc.Devocional)
                   .HasForeignKey(dc => dc.DevocionalId);
        }
    }
}