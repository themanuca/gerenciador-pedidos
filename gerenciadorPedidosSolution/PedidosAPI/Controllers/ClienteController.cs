using App.DTOs;
using App.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PedidosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService) 
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var clientes = await _clienteService.GetAllClientes();
                return Ok(clientes);

            } catch (Exception) {
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteById(new ClienteDTO { Id = id });
                return Ok(cliente);
            }
            catch (Exception) {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClienteDTO clienteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _clienteService.CreateCliente(clienteDto);
                return Ok();
            }
            catch(Exception) 
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, ClienteDTO clienteDto)
        {
            try
            {
                clienteDto.Id = id;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _clienteService.UpdateCliente(clienteDto);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _clienteService.DeleteCliente(id);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
