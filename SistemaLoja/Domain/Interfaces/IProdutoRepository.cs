using System;
using SistemaLoja.Domain.Entities;

namespace SistemaLoja.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<Produto> ObterPorIdAsync(int id);
    Task<IEnumerable<Produto>> ObterTodosAsync();
    Task<IEnumerable<Produto>> ObterAtivosAsync();
    Task AdicionarAsync(Produto produto);
    Task AtualizarAsync(Produto produto);
    Task<bool> ExisteAsync(int id);
}

