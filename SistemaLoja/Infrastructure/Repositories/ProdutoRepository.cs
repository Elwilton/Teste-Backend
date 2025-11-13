using System;
using Microsoft.EntityFrameworkCore;
using SistemaLoja.Domain.Entities;
using SistemaLoja.Domain.Interfaces;
using SistemaLoja.Infrastructure.Data;

namespace SistemaLoja.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly LojaDbContext _context;

    public ProdutoRepository(LojaDbContext context)
    {
        _context = context;
    }

    public async Task<Produto> ObterPorIdAsync(int id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        return await _context.Produtos
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Produto>> ObterAtivosAsync()
    {
        return await _context.Produtos
            .Where(p => p.Ativo)
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteAsync(int id)
    {
        return await _context.Produtos.AnyAsync(p => p.Id == id);
    }
}

