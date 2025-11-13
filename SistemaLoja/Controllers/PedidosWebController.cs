using System;
using Microsoft.AspNetCore.Mvc;
using SistemaLoja.Application.DTOs;
using SistemaLoja.Application.Services;

namespace SistemaLoja.Controllers;

[Route("[controller]/[action]")]
public class PedidosWebController : Controller
{
    private readonly PedidoService _pedidoService;
    private readonly ProdutoService _produtoService;

    public PedidosWebController(PedidoService pedidoService, ProdutoService produtoService)
    {
        _pedidoService = pedidoService;
        _produtoService = produtoService;
    }

    // GET: /PedidosWeb
    public async Task<IActionResult> Index()
    {
        var pedidos = await _pedidoService.ObterTodosAsync();
        return View(pedidos);
    }

    // GET: /PedidosWeb/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id);
        if (pedido == null)
        {
            TempData["ErrorMessage"] = "Pedido não encontrado.";
            return RedirectToAction(nameof(Index));
        }

        return View(pedido);
    }

    // GET: /PedidosWeb/Create
    public async Task<IActionResult> Create()
    {
        var produtos = await _produtoService.ObterTodosAsync();
        ViewBag.Produtos = produtos;
        return View(new CriarPedidoDto());
    }

    // POST: /PedidosWeb/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CriarPedidoDto dto)
    {
        if (dto.Itens == null || !dto.Itens.Any(i => i.Quantidade > 0))
        {
            ModelState.AddModelError("", "Selecione ao menos um produto e quantidade válida.");
            var produtos = await _produtoService.ObterTodosAsync();
            ViewBag.Produtos = produtos;
            return View(dto);
        }

        try
        {
            await _pedidoService.CriarAsync(dto);
            TempData["SuccessMessage"] = "Pedido criado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Erro ao criar pedido: {ex.Message}";
            var produtos = await _produtoService.ObterTodosAsync();
            ViewBag.Produtos = produtos;
            return View(dto);
        }
    }
}


