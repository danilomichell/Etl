using Microsoft.EntityFrameworkCore;
using Etl.Data.Domain.Entities;

namespace Etl.Data.Context
{
    public partial class FolhaContext : DbContext
    {
        public FolhaContext(DbContextOptions<FolhaContext> options)
               : base(options)
        {
        }

        public virtual DbSet<Cargos> Cargos { get; set; } = null!;
        public virtual DbSet<Carreiras> Carreiras { get; set; } = null!;
        public virtual DbSet<Colaboradores> Colaboradores { get; set; } = null!;
        public virtual DbSet<DmCargos> DmCargos { get; set; } = null!;
        public virtual DbSet<DmFaixasEtarias> DmFaixasEtarias { get; set; } = null!;
        public virtual DbSet<DmRubricas> DmRubricas { get; set; } = null!;
        public virtual DbSet<DmSetores> DmSetores { get; set; } = null!;
        public virtual DbSet<DmTemposFolhas> DmTemposFolhas { get; set; } = null!;
        public virtual DbSet<DmTemposServicos> DmTemposServicos { get; set; } = null!;
        public virtual DbSet<EvolucoesFuncionais> EvolucoesFuncionais { get; set; } = null!;
        public virtual DbSet<FolhasPagamentos> FolhasPagamentos { get; set; } = null!;
        public virtual DbSet<FtLancamentos> FtLancamentos { get; set; } = null!;
        public virtual DbSet<GruposRubricas> GruposRubricas { get; set; } = null!;
        public virtual DbSet<Lancamentos> Lancamentos { get; set; } = null!;
        public virtual DbSet<Rubricas> Rubricas { get; set; } = null!;
        public virtual DbSet<Setores> Setores { get; set; } = null!;
        public virtual DbSet<Unidades> Unidades { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargos>(entity =>
            {
                entity.HasKey(e => e.CodCargo)
                    .HasName("cargos_pk");

                entity.ToTable("cargos", "folha");

                entity.Property(e => e.CodCargo)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_cargo");

                entity.Property(e => e.CodCarreira).HasColumnName("cod_carreira");

                entity.Property(e => e.DscCargo)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_cargo");

                entity.HasOne(d => d.CodCarreiraNavigation)
                    .WithMany(p => p.Cargos)
                    .HasForeignKey(d => d.CodCarreira)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("cargos_carreiras_fk");
            });

            modelBuilder.Entity<Carreiras>(entity =>
            {
                entity.HasKey(e => e.CodCarreira)
                    .HasName("carreiras_pk");

                entity.ToTable("carreiras", "folha");

                entity.Property(e => e.CodCarreira)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_carreira");

                entity.Property(e => e.DscCarreira)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_carreira");
            });

            modelBuilder.Entity<Colaboradores>(entity =>
            {
                entity.HasKey(e => e.CodColab)
                    .HasName("colaboradores_pk");

                entity.ToTable("colaboradores", "folha");

                entity.Property(e => e.CodColab)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_colab");

                entity.Property(e => e.DatAdmissao).HasColumnName("dat_admissao");

                entity.Property(e => e.DatNasc).HasColumnName("dat_nasc");

                entity.Property(e => e.NomColab)
                    .HasMaxLength(100)
                    .HasColumnName("nom_colab");
            });

            modelBuilder.Entity<DmCargos>(entity =>
            {
                entity.HasKey(e => e.CodCargo)
                    .HasName("dm_cargos_pk");

                entity.ToTable("dm_cargos", "folhadw");

                entity.Property(e => e.CodCargo)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_cargo");

                entity.Property(e => e.DscCargo)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_cargo");

                entity.Property(e => e.DscCarreira)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_carreira");
            });

            modelBuilder.Entity<DmFaixasEtarias>(entity =>
            {
                entity.HasKey(e => e.CodFaixa)
                    .HasName("dm_faixas_etarias_pk");

                entity.ToTable("dm_faixas_etarias", "folhadw");

                entity.Property(e => e.CodFaixa)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_faixa");

                entity.Property(e => e.DscFaixa)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_faixa");

                entity.Property(e => e.IdadeFinal).HasColumnName("idade_final");

                entity.Property(e => e.IdadeInicial).HasColumnName("idade_inicial");
            });

            modelBuilder.Entity<DmRubricas>(entity =>
            {
                entity.HasKey(e => e.CodRubrica)
                    .HasName("dm_rubricas_pk");

                entity.ToTable("dm_rubricas", "folhadw");

                entity.Property(e => e.CodRubrica)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_rubrica");

                entity.Property(e => e.DscGrupo)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_grupo");

                entity.Property(e => e.DscRubrica)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_rubrica");

                entity.Property(e => e.TipoRubrica)
                    .HasMaxLength(100)
                    .HasColumnName("tipo_rubrica");
            });

            modelBuilder.Entity<DmSetores>(entity =>
            {
                entity.HasKey(e => e.CodSetor)
                    .HasName("dm_setores_pk");

                entity.ToTable("dm_setores", "folhadw");

                entity.Property(e => e.CodSetor)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_setor");

                entity.Property(e => e.CidadeUnidade)
                    .HasMaxLength(100)
                    .HasColumnName("cidade_unidade");

                entity.Property(e => e.DscSetor)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_setor");

                entity.Property(e => e.DscUnidade)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_unidade");

                entity.Property(e => e.UfUnidade)
                    .HasMaxLength(1)
                    .HasColumnName("uf_unidade");
            });

            modelBuilder.Entity<DmTemposFolhas>(entity =>
            {
                entity.HasKey(e => e.IdAnoMes)
                    .HasName("dm_tempos_folhas_pk");

                entity.ToTable("dm_tempos_folhas", "folhadw");

                entity.Property(e => e.IdAnoMes)
                    .ValueGeneratedNever()
                    .HasColumnName("id_ano_mes");

                entity.Property(e => e.Ano).HasColumnName("ano");

                entity.Property(e => e.Mes).HasColumnName("mes");
            });

            modelBuilder.Entity<DmTemposServicos>(entity =>
            {
                entity.HasKey(e => e.CodTempoServ)
                    .HasName("dm_tempos_servicos_pk");

                entity.ToTable("dm_tempos_servicos", "folhadw");

                entity.Property(e => e.CodTempoServ)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_tempo_serv");

                entity.Property(e => e.AnoFinal).HasColumnName("ano_final");

                entity.Property(e => e.AnoInicial).HasColumnName("ano_inicial");

                entity.Property(e => e.DscTempoServ)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_tempo_serv");
            });

            modelBuilder.Entity<EvolucoesFuncionais>(entity =>
            {
                entity.HasKey(e => new { e.CodColab, e.DatIni })
                    .HasName("evolucoes_funcionais_pk");

                entity.ToTable("evolucoes_funcionais", "folha");

                entity.Property(e => e.CodColab).HasColumnName("cod_colab");

                entity.Property(e => e.DatIni).HasColumnName("dat_ini");

                entity.Property(e => e.CodCargo).HasColumnName("cod_cargo");

                entity.Property(e => e.CodSetor).HasColumnName("cod_setor");

                entity.HasOne(d => d.CodCargoNavigation)
                    .WithMany(p => p.EvolucoesFuncionais)
                    .HasForeignKey(d => d.CodCargo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("evo_cargos_fk");

                entity.HasOne(d => d.CodColabNavigation)
                    .WithMany(p => p.EvolucoesFuncionais)
                    .HasForeignKey(d => d.CodColab)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("evo_colab_fk");

                entity.HasOne(d => d.CodSetorNavigation)
                    .WithMany(p => p.EvolucoesFuncionais)
                    .HasForeignKey(d => d.CodSetor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("evo_setores_fk");
            });

            modelBuilder.Entity<FolhasPagamentos>(entity =>
            {
                entity.HasKey(e => new { e.Ano, e.Mes, e.TpoFolha })
                    .HasName("fol_pgto_pk");

                entity.ToTable("folhas_pagamentos", "folha");

                entity.Property(e => e.Ano).HasColumnName("ano");

                entity.Property(e => e.Mes).HasColumnName("mes");

                entity.Property(e => e.TpoFolha)
                    .HasMaxLength(1)
                    .HasColumnName("tpo_folha");

                entity.Property(e => e.DscFolha)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_folha");
            });

            modelBuilder.Entity<FtLancamentos>(entity =>
            {
                entity.HasKey(e => new { e.CodRubrica, e.CodSetor, e.CodCargo, e.CodFaixa, e.CodTempoServ, e.IdAnoMes })
                    .HasName("ft_lancamentos_pk");

                entity.ToTable("ft_lancamentos", "folhadw");

                entity.Property(e => e.CodRubrica).HasColumnName("cod_rubrica");

                entity.Property(e => e.CodSetor).HasColumnName("cod_setor");

                entity.Property(e => e.CodCargo).HasColumnName("cod_cargo");

                entity.Property(e => e.CodFaixa).HasColumnName("cod_faixa");

                entity.Property(e => e.CodTempoServ).HasColumnName("cod_tempo_serv");

                entity.Property(e => e.IdAnoMes).HasColumnName("id_ano_mes");

                entity.Property(e => e.TotalLanc).HasColumnName("total_lanc");

                entity.Property(e => e.ValorBruto).HasColumnName("valor_bruto");

                entity.Property(e => e.ValorDesconto).HasColumnName("valor_desconto");

                entity.Property(e => e.ValorLiquido).HasColumnName("valor_liquido");

                entity.HasOne(d => d.CodCargoNavigation)
                    .WithMany(p => p.FtLancamentos)
                    .HasForeignKey(d => d.CodCargo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("ft_lanc_cargo_fk");

                entity.HasOne(d => d.CodFaixaNavigation)
                    .WithMany(p => p.FtLancamentos)
                    .HasForeignKey(d => d.CodFaixa)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("ft_lanc_faixa_fk");

                entity.HasOne(d => d.CodRubricaNavigation)
                    .WithMany(p => p.FtLancamentos)
                    .HasForeignKey(d => d.CodRubrica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("ft_lanc_rubrica_fk");

                entity.HasOne(d => d.CodSetorNavigation)
                    .WithMany(p => p.FtLancamentos)
                    .HasForeignKey(d => d.CodSetor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("ft_lanc_setor_fk");

                entity.HasOne(d => d.CodTempoServNavigation)
                    .WithMany(p => p.FtLancamentos)
                    .HasForeignKey(d => d.CodTempoServ)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("ft_lanc_tempo_serv_fk");

                entity.HasOne(d => d.IdAnoMesNavigation)
                    .WithMany(p => p.FtLancamentos)
                    .HasForeignKey(d => d.IdAnoMes)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("ft_lanc_tempo_folha_fk");
            });

            modelBuilder.Entity<GruposRubricas>(entity =>
            {
                entity.HasKey(e => e.CodGrupo)
                    .HasName("grupos_rubricas_pk");

                entity.ToTable("grupos_rubricas", "folha");

                entity.Property(e => e.CodGrupo)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_grupo");

                entity.Property(e => e.DscGrupo)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_grupo");
            });

            modelBuilder.Entity<Lancamentos>(entity =>
            {
                entity.HasKey(e => new { e.Ano, e.Mes, e.TpoFolha, e.CodRubrica, e.CodColab })
                    .HasName("lancamentos_pk");

                entity.ToTable("lancamentos", "folha");

                entity.Property(e => e.Ano).HasColumnName("ano");

                entity.Property(e => e.Mes).HasColumnName("mes");

                entity.Property(e => e.TpoFolha)
                    .HasMaxLength(1)
                    .HasColumnName("tpo_folha");

                entity.Property(e => e.CodRubrica).HasColumnName("cod_rubrica");

                entity.Property(e => e.CodColab).HasColumnName("cod_colab");

                entity.Property(e => e.DatLanc).HasColumnName("dat_lanc");

                entity.Property(e => e.ValLanc).HasColumnName("val_lanc");

                entity.HasOne(d => d.CodColabNavigation)
                    .WithMany(p => p.Lancamentos)
                    .HasForeignKey(d => d.CodColab)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("lanc_colab_fk");

                entity.HasOne(d => d.CodRubricaNavigation)
                    .WithMany(p => p.Lancamentos)
                    .HasForeignKey(d => d.CodRubrica)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("lanc_rub_fk");

                entity.HasOne(d => d.FolhasPagamentos)
                    .WithMany(p => p.Lancamentos)
                    .HasForeignKey(d => new { d.Ano, d.Mes, d.TpoFolha })
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("lanc_folha_fk");
            });

            modelBuilder.Entity<Rubricas>(entity =>
            {
                entity.HasKey(e => e.CodRubrica)
                    .HasName("rubricas_pk");

                entity.ToTable("rubricas", "folha");

                entity.Property(e => e.CodRubrica)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_rubrica");

                entity.Property(e => e.CodGrupo).HasColumnName("cod_grupo");

                entity.Property(e => e.DscRubrica)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_rubrica");

                entity.Property(e => e.TpoRubrica)
                    .HasMaxLength(1)
                    .HasColumnName("tpo_rubrica");

                entity.HasOne(d => d.CodGrupoNavigation)
                    .WithMany(p => p.Rubricas)
                    .HasForeignKey(d => d.CodGrupo)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("rub_grupo_fk");
            });

            modelBuilder.Entity<Setores>(entity =>
            {
                entity.HasKey(e => e.CodSetor)
                    .HasName("setores_pk");

                entity.ToTable("setores", "folha");

                entity.Property(e => e.CodSetor)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_setor");

                entity.Property(e => e.CodColabChefe).HasColumnName("cod_colab_chefe");

                entity.Property(e => e.CodUnd).HasColumnName("cod_und");

                entity.Property(e => e.DscSetor)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_setor");

                entity.HasOne(d => d.CodColabChefeNavigation)
                    .WithMany(p => p.Setores)
                    .HasForeignKey(d => d.CodColabChefe)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("setores_colab_fk");

                entity.HasOne(d => d.CodUndNavigation)
                    .WithMany(p => p.Setores)
                    .HasForeignKey(d => d.CodUnd)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("setores_unidades_fk");
            });

            modelBuilder.Entity<Unidades>(entity =>
            {
                entity.HasKey(e => e.CodUnd)
                    .HasName("unidades_pk");

                entity.ToTable("unidades", "folha");

                entity.Property(e => e.CodUnd)
                    .ValueGeneratedNever()
                    .HasColumnName("cod_und");

                entity.Property(e => e.CidUnd)
                    .HasMaxLength(40)
                    .HasColumnName("cid_und");

                entity.Property(e => e.DscUnd)
                    .HasMaxLength(100)
                    .HasColumnName("dsc_und");

                entity.Property(e => e.UfUnd)
                    .HasMaxLength(2)
                    .HasColumnName("uf_und")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
