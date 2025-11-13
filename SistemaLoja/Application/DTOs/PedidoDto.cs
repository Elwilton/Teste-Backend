using System;
namespace SistemaLoja.Application.DTOs;

public class PedidoDto
{
    public int Id { get; set; }
    public DateTime DataPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<PedidoItemDto>? Itens { get; set; }
}

public class PedidoItemDto
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public string NomeProduto { get; set; } = string.Empty;
    public decimal PrecoUnitario { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorTotal { get; set; }
}

public class CriarPedidoDto
{
    public List<ItemPedidoDto> Itens { get; set; } = new();
}

public class ItemPedidoDto
{
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
}

