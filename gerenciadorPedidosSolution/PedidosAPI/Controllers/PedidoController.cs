using App.DTOs;
using App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PedidosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<ActionResult<PedidoDTO>> CreatePedido(PedidoDTO pedidoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pedidoCriado = await _pedidoService.CreatePedido(pedidoDto);
                return Ok(pedidoCriado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoItemDetaisDTO>>> GetAllPedidos()
        {
            try
            {
                var pedidos = await _pedidoService.GetAllPedidos();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter todos os pedidos: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao obter pedidos.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDetaisDTO>> GetPedidoById(int id)
        {
            try
            {
                var pedido = await _pedidoService.GetPedidoById(id);
                if (pedido == null)
                {
                    return NotFound();
                }
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetPedidosByClienteId(int clienteId)
        {
            try
            {
                var pedidos = await _pedidoService.GetPedidosByClienteId(clienteId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter pedidos por ClienteId: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao obter pedidos.");
            }
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetPedidosByStatus(string status)
        {
            try
            {
                var pedidos = await _pedidoService.GetPedidosByStatus(status);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatusPedido(PedidoDTO pedidoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _pedidoService.UpdateStatusPedido(pedidoDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
