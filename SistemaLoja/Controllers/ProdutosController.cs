using Microsoft.AspNetCore.Mvc;
using SistemaLoja.Application.DTOs;
using SistemaLoja.Application.Services;

namespace SistemaLoja.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _produtoService;

    public ProdutosController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll()
    {
        try
        {
            var produtos = await _produtoService.ObterTodosAsync();
            return Ok(produtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar produtos", error = ex.Message });
        }
    }

    [HttpGet("ativos")]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAtivos()
    {
        try
        {
            var produtos = await _produtoService.ObterAtivosAsync();
            return Ok(produtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar produtos ativos", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoDto>> GetById(int id)
    {
        try
        {
            var produto = await _produtoService.ObterPorIdAsync(id);
            if (produto == null)
                return NotFound(new { message = "Produto não encontrado" });

            return Ok(produto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar produto", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> Create([FromBody] CriarProdutoDto dto)
    {
        try
        {
            var produto = await _produtoService.CriarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao criar produto", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProdutoDto>> Update(int id, [FromBody] AtualizarProdutoDto dto)
    {
        try
        {
            var produto = await _produtoService.AtualizarAsync(id, dto);
            return Ok(produto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao atualizar produto", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _produtoService.InativarAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao inativar produto", error = ex.Message });
        }
    }
}

