using App.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IClienteService
    {
        Task CreateCliente(ClienteDTO clienteDto);
        Task UpdateCliente(ClienteDTO clienteDto);
        Task DeleteCliente(int clienteId);
        Task<ClienteDTO> GetClienteById(ClienteDTO clienteId);
        Task<IEnumerable<ClienteDTO>> GetAllClientes();
    }
}
