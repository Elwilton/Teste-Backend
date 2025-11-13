using System;
using SistemaLoja.Domain.Entities;

namespace SistemaLoja.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido> ObterPorIdAsync(int id);
    Task<Pedido> ObterPorIdComItensAsync(int id);
    Task<IEnumerable<Pedido>> ObterTodosAsync();
    Task<IEnumerable<Pedido>> ObterTodosComItensAsync();
    Task AdicionarAsync(Pedido pedido);
    Task AtualizarAsync(Pedido pedido);
}