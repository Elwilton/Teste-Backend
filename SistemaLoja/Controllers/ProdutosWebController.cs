using Microsoft.AspNetCore.Mvc;
using SistemaLoja.Application.DTOs;
using SistemaLoja.Application.Services;

namespace SistemaLoja.Controllers;

[Route("[controller]")]
public class ProdutosWebController : Controller
{
    private readonly ProdutoService _produtoService;

    public ProdutosWebController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var produtos = await _produtoService.ObterTodosAsync();
        return View(produtos);
    }

    [HttpGet("Create")]
    public IActionResult Create() => View(new CriarProdutoDto());

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CriarProdutoDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _produtoService.CriarAsync(dto);
        TempData["SuccessMessage"] = "Produto criado com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var produto = await _produtoService.ObterPorIdAsync(id);
        if (produto == null)
            return NotFound();

        var dto = new AtualizarProdutoDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco
        };

        return View(dto);
    }

    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AtualizarProdutoDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        await _produtoService.AtualizarAsync(dto.Id, dto);
        TempData["SuccessMessage"] = "Produto atualizado com sucesso!";
        return RedirectToAction(nameof(Index));
    }
}
