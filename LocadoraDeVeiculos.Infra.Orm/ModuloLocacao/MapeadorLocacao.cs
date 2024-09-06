using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloLocacao;

public class MapeadorLocacao : IEntityTypeConfiguration<Locacao>
{
    public void Configure(EntityTypeBuilder<Locacao> builder)
    {
        builder.ToTable("TBLocacao");

        builder.Property(l => l.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(l => l.TipoPlano)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(l => l.MarcadorCombustivel)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(l => l.QuilometragemPercorrida)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(l => l.DataLocacao)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(l => l.DevolucaoPrevista)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(l => l.DataDevolucao)
            .HasColumnType("datetime2")
            .IsRequired(false);

        builder.Property(l => l.VeiculoId)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(l => l.Veiculo)
            .WithMany()
            .HasForeignKey(l => l.VeiculoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(l => l.CondutorId)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(l => l.Condutor)
            .WithMany()
            .HasForeignKey(l => l.CondutorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(l => l.ConfiguracaoCombustivelId)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(l => l.ConfiguracaoCombustivel)
            .WithMany()
            .HasForeignKey(l => l.ConfiguracaoCombustivelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(l => l.TaxasSelecionadas)
            .WithMany(t => t.Locacoes)
            .UsingEntity(j => j.ToTable("TBLocacaoTaxa"));

        builder.Property(s => s.EmpresaId)
            .HasColumnType("int")
            .HasColumnName("Empresa_Id");

        builder.HasOne(g => g.Empresa)
            .WithMany()
            .HasForeignKey(s => s.EmpresaId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}