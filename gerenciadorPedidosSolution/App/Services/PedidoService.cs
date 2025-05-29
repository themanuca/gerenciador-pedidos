using App.DTOs;
using App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class PedidoService : IPedidoService
    {
        public Task<PedidoDTO> CreatePedido(PedidoDTO pedidoDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<PedidoItemDetaisDTO>> GetAllPedidos()
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> GetPedidoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoDTO>> GetPedidosByClienteId(int clienteId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoDTO>> GetPedidosByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStatusPedido(PedidoDTO pedidoDto)
        {
            throw new NotImplementedException();
        }
    }
}
