using System;
using SistemaLoja.Application.DTOs;
using SistemaLoja.Domain.Entities;
using SistemaLoja.Domain.Interfaces;

namespace SistemaLoja.Application.Services;

public class ProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<ProdutoDto>> ObterTodosAsync()
    {
        var produtos = await _produtoRepository.ObterTodosAsync();
        return produtos.Select(p => new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            DataCadastro = p.DataCadastro,
            Ativo = p.Ativo
        });
    }

    public async Task<IEnumerable<ProdutoDto>> ObterAtivosAsync()
    {
        var produtos = await _produtoRepository.ObterAtivosAsync();
        return produtos.Select(p => new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            DataCadastro = p.DataCadastro,
            Ativo = p.Ativo
        });
    }

    public async Task<ProdutoDto> ObterPorIdAsync(int id)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id) ?? throw new KeyNotFoundException($"Produto com ID {id} não encontrado.");

        return new ProdutoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            DataCadastro = produto.DataCadastro,
            Ativo = produto.Ativo
        };
    }

    public async Task<ProdutoDto> CriarAsync(CriarProdutoDto dto)
    {
        var produto = new Produto(dto.Nome, dto.Descricao, dto.Preco);
        await _produtoRepository.AdicionarAsync(produto);

        return new ProdutoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            DataCadastro = produto.DataCadastro,
            Ativo = produto.Ativo
        };
    }

    public async Task<ProdutoDto> AtualizarAsync(int id, AtualizarProdutoDto dto)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);
        if (produto == null)
            throw new Exception("Produto não encontrado");

        produto.Atualizar(dto.Nome, dto.Descricao, dto.Preco);
        await _produtoRepository.AtualizarAsync(produto);

        return new ProdutoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            DataCadastro = produto.DataCadastro,
            Ativo = produto.Ativo
        };
    }

    public async Task InativarAsync(int id)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);
        if (produto == null)
            throw new Exception("Produto não encontrado");

        produto.Inativar();
        await _produtoRepository.AtualizarAsync(produto);
    }
}

