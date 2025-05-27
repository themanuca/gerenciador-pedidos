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
        public async Task<ActionResult> IndexAsync()
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
        public async Task<ActionResult> DetailsAsync(int id)
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
               await _clienteService.CreateCliente(clienteDto);
                return Ok();
            }
            catch(Exception) 
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<ActionResult> EditAsync(ClienteDTO clienteDto)
        {
            try
            {
                await _clienteService.UpdateCliente(clienteDto);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
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
