using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaLoja.Application.DTOs;

public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public DateTime DataCadastro { get; set; }
    public bool Ativo { get; set; }
}

public class CriarProdutoDto
{
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;
    [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }
}

public class AtualizarProdutoDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;
    [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }
}

