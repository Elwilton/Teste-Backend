using System;
namespace SistemaLoja.Domain.Entities;

public class Pedido
{
    public int Id { get; private set; }
    public DateTime DataPedido { get; private set; }
    public decimal ValorTotal { get; private set; }
    public string Status { get; private set; }

    private List<PedidoItem> _itens;
    public IReadOnlyCollection<PedidoItem> Itens => _itens.AsReadOnly();

    protected Pedido()
    {
        _itens = new List<PedidoItem>();
    }

    public Pedido(List<PedidoItem> itens)
    {
        if (itens == null || !itens.Any())
            throw new ArgumentException("Pedido deve conter ao menos um item");

        _itens = itens;
        DataPedido = DateTime.Now;
        Status = "Pendente";
        CalcularValorTotal();
    }

    public void AdicionarItem(Produto produto, int quantidade)
    {
        if (produto == null)
            throw new ArgumentNullException(nameof(produto));

        if (quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero");

        var itemExistente = _itens.FirstOrDefault(i => i.ProdutoId == produto.Id);

        if (itemExistente != null)
        {
            itemExistente.AtualizarQuantidade(itemExistente.Quantidade + quantidade);
        }
        else
        {
            _itens.Add(new PedidoItem(produto, quantidade));
        }

        CalcularValorTotal();
    }

    public void RemoverItem(int produtoId)
    {
        var item = _itens.FirstOrDefault(i => i.ProdutoId == produtoId);
        if (item != null)
        {
            _itens.Remove(item);
            CalcularValorTotal();
        }
    }

    private void CalcularValorTotal()
    {
        ValorTotal = _itens.Sum(i => i.ValorTotal);
    }

    public void FinalizarPedido()
    {
        if (!_itens.Any())
            throw new InvalidOperationException("Não é possível finalizar um pedido sem itens");

        Status = "Finalizado";
    }

    public void CancelarPedido()
    {
        Status = "Cancelado";
    }
}

