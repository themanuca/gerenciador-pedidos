using App.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoDTO> CreatePedido(PedidoDTO pedidoDto);
        Task<List<PedidoItemDetaisDTO>> GetAllPedidos();
        Task UpdateStatusPedido(PedidoDTO pedidoDto);
        Task<PedidoDTO> GetPedidoById(int id);
        Task<IEnumerable<PedidoDTO>> GetPedidosByClienteId(int clienteId);
        Task<IEnumerable<PedidoDTO>> GetPedidosByStatus(string status);



    }
}
