using Microsoft.EntityFrameworkCore;
using SistemaLoja.Domain.Entities;
using SistemaLoja.Domain.Interfaces;
using SistemaLoja.Infrastructure.Data;

namespace SistemaLoja.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly LojaDbContext _context;

    public PedidoRepository(LojaDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido> ObterPorIdAsync(int id)
    {
        return await _context.Pedidos.FindAsync(id);
    }

    public async Task<Pedido> ObterPorIdComItensAsync(int id)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Pedido>> ObterTodosAsync()
    {
        return await _context.Pedidos
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();
    }

    public async Task<IEnumerable<Pedido>> ObterTodosComItensAsync()
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .OrderByDescending(p => p.DataPedido)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Pedido pedido)
    {
        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync();
    }
}