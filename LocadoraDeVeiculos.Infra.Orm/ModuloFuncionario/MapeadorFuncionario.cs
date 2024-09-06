using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloFuncionario;

public class MapeadorFuncionario : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("TBFuncionario");

        builder.Property(c => c.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(c => c.NomeCompleto)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(c => c.Admissao)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(c => c.Salario)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(c => c.EmpresaId)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(c => c.Empresa)
            .WithMany()
            .HasForeignKey(f => f.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.UsuarioId)
            .HasColumnType("int")
            .IsRequired();
    }
}