using System;
using SistemaLoja.Application.DTOs;
using SistemaLoja.Domain.Entities;
using SistemaLoja.Domain.Interfaces;

namespace SistemaLoja.Application.Services;

public class PedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoRepository _produtoRepository;

    public PedidoService(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
    {
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<PedidoDto>> ObterTodosAsync()
    {
        var pedidos = await _pedidoRepository.ObterTodosComItensAsync();
        return pedidos.Select(p => new PedidoDto
        {
            Id = p.Id,
            DataPedido = p.DataPedido,
            ValorTotal = p.ValorTotal,
            Status = p.Status,
            Itens = p.Itens.Select(i => new PedidoItemDto
            {
                Id = i.Id,
                ProdutoId = i.ProdutoId,
                NomeProduto = i.NomeProduto,
                PrecoUnitario = i.PrecoUnitario,
                Quantidade = i.Quantidade,
                ValorTotal = i.ValorTotal
            }).ToList()
        });
    }

    public async Task<PedidoDto> ObterPorIdAsync(int id)
    {
        var pedido = await _pedidoRepository.ObterPorIdComItensAsync(id);
        if (pedido == null)
            return null;

        return new PedidoDto
        {
            Id = pedido.Id,
            DataPedido = pedido.DataPedido,
            ValorTotal = pedido.ValorTotal,
            Status = pedido.Status,
            Itens = pedido.Itens.Select(i => new PedidoItemDto
            {
                Id = i.Id,
                ProdutoId = i.ProdutoId,
                NomeProduto = i.NomeProduto,
                PrecoUnitario = i.PrecoUnitario,
                Quantidade = i.Quantidade,
                ValorTotal = i.ValorTotal
            }).ToList()
        };
    }

    public async Task<PedidoDto> CriarAsync(CriarPedidoDto dto)
    {
        var itens = new List<PedidoItem>();

        foreach (var itemDto in dto.Itens)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(itemDto.ProdutoId);
            if (produto == null)
                throw new Exception($"Produto {itemDto.ProdutoId} não encontrado");

            if (!produto.Ativo)
                throw new Exception($"Produto {produto.Nome} está inativo");

            itens.Add(new PedidoItem(produto, itemDto.Quantidade));
        }

        var pedido = new Pedido(itens);
        await _pedidoRepository.AdicionarAsync(pedido);

        return await ObterPorIdAsync(pedido.Id);
    }
}

