using Microsoft.AspNetCore.Mvc;
using SistemaLoja.Application.DTOs;
using SistemaLoja.Application.Services;

namespace SistemaLoja.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _pedidoService;

    public PedidosController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoDto>>> GetAll()
    {
        try
        {
            var pedidos = await _pedidoService.ObterTodosAsync();
            return Ok(pedidos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar pedidos", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoDto>> GetById(int id)
    {
        try
        {
            var pedido = await _pedidoService.ObterPorIdAsync(id);
            if (pedido == null)
                return NotFound(new { message = "Pedido não encontrado" });

            return Ok(pedido);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar pedido", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<PedidoDto>> Create([FromBody] CriarPedidoDto dto)
    {
        try
        {
            var pedido = await _pedidoService.CriarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao criar pedido", error = ex.Message });
        }
    }
}

