using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloGrupoVeiculos;

public class MapeadorGrupoVeiculos : IEntityTypeConfiguration<GrupoVeiculos>
{
    public void Configure(EntityTypeBuilder<GrupoVeiculos> builder)
    {
        builder.ToTable("TBGrupoVeiculos");

        builder.Property(g => g.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(g => g.Nome)
            .HasColumnType("varchar(100)")
            .IsRequired();
    }
}