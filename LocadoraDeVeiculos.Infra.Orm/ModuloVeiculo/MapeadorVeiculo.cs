using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloVeiculo;

public class MapeadorVeiculo : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.ToTable("TBVeiculo");

        builder.Property(v => v.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(v => v.Modelo)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(v => v.Marca)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(v => v.TipoCombustivel)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(v => v.CapacidadeTanque)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(v => v.Foto)
            .HasColumnType("varbinary(max)")
            .HasDefaultValue(Array.Empty<byte>());

        builder.Property(v => v.GrupoVeiculosId)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(v => v.GrupoVeiculos)
            .WithMany(g => g.Veiculos)
            .HasForeignKey(v => v.GrupoVeiculosId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(s => s.EmpresaId)
            .HasColumnType("int")
            .HasColumnName("Empresa_Id")
            .IsRequired();

        builder.HasOne(g => g.Empresa)
            .WithMany()
            .HasForeignKey(s => s.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}