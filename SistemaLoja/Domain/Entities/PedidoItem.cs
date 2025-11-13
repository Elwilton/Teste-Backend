using System;
namespace SistemaLoja.Domain.Entities;

public class PedidoItem
{
    public int Id { get; private set; }
    public int PedidoId { get; private set; }
    public int ProdutoId { get; private set; }
    public string NomeProduto { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorTotal { get; private set; }

    // Navegação
    public Produto Produto { get; private set; }
    public Pedido Pedido { get; private set; }

    protected PedidoItem() { }

    public PedidoItem(Produto produto, int quantidade)
    {
        if (produto == null)
            throw new ArgumentNullException(nameof(produto));

        if (quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero");

        ProdutoId = produto.Id;
        NomeProduto = produto.Nome;
        PrecoUnitario = produto.Preco;
        Quantidade = quantidade;
        CalcularValorTotal();
    }

    public void AtualizarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero");

        Quantidade = quantidade;
        CalcularValorTotal();
    }

    private void CalcularValorTotal()
    {
        ValorTotal = PrecoUnitario * Quantidade;
    }
}


