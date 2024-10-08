﻿using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloPlanoCobranca;

public class MapeadorPlanoCobranca : IEntityTypeConfiguration<PlanoCobranca>
{
    public void Configure(EntityTypeBuilder<PlanoCobranca> builder)
    {
        builder.ToTable("TBPlanoCobranca");

        builder.Property(p => p.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(p => p.PrecoDiarioPlanoDiario)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.PrecoQuilometroPlanoDiario)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.QuilometrosDisponiveisPlanoControlado)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.PrecoDiarioPlanoControlado)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.PrecoQuilometroExtrapoladoPlanoControlado)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.PrecoDiarioPlanoLivre)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.GrupoVeiculosId)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(p => p.GrupoVeiculos)
            .WithMany()
            .HasForeignKey(p => p.GrupoVeiculosId)
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