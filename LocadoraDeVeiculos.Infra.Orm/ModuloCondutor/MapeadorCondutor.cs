using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloCondutor;

public class MapeadorCondutor : IEntityTypeConfiguration<Condutor>
{
    public void Configure(EntityTypeBuilder<Condutor> builder)
    {
        builder.ToTable("TBCondutor");

        builder.Property(c => c.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(c => c.Nome)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(c => c.Telefone)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(c => c.CPF)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(c => c.CNH)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(c => c.ValidadeCNH)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(c => c.ClienteCondutor)
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(c => c.ClienteId)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(c => c.Cliente)
            .WithMany(cl => cl.Condutores)
            .HasForeignKey(c => c.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}