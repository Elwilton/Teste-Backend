using Microsoft.EntityFrameworkCore;
using SistemaLoja.Domain.Entities;

namespace SistemaLoja.Infrastructure.Data;

public class LojaDbContext : DbContext
{
    public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("Produtos");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            entity.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(p => p.Preco)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.DataCadastro)
                .IsRequired();

            entity.Property(p => p.Ativo)
                .IsRequired();
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.ToTable("Pedidos");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            entity.Property(p => p.DataPedido)
                .IsRequired();

            entity.Property(p => p.ValorTotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PedidoItem>(entity =>
        {
            entity.ToTable("PedidoItens");
            entity.HasKey(pi => pi.Id);

            entity.Property(pi => pi.Id)
                .ValueGeneratedOnAdd();

            entity.Property(pi => pi.NomeProduto)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(pi => pi.PrecoUnitario)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(pi => pi.Quantidade)
                .IsRequired();

            entity.Property(pi => pi.ValorTotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.HasOne(pi => pi.Produto)
                .WithMany()
                .HasForeignKey(pi => pi.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

