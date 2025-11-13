using System;
namespace SistemaLoja.Domain.Entities;

public class Produto
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public bool Ativo { get; private set; }

    // Construtor para EF Core
    protected Produto() { }

    public Produto(string nome, string descricao, decimal preco)
    {
        ValidarDados(nome, descricao, preco);

        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        DataCadastro = DateTime.Now;
        Ativo = true;
    }

    public void Atualizar(string nome, string descricao, decimal preco)
    {
        ValidarDados(nome, descricao, preco);

        Nome = nome;
        Descricao = descricao;
        Preco = preco;
    }

    public void Inativar() => Ativo = false;
    public void Ativar() => Ativo = true;

    private void ValidarDados(string nome, string descricao, decimal preco)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório");

        if (nome.Length > 100)
            throw new ArgumentException("Nome deve ter no máximo 100 caracteres");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória");

        if (preco <= 0)
            throw new ArgumentException("Preço deve ser maior que zero");
    }
}
