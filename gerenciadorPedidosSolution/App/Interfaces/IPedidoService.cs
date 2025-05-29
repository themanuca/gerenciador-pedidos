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
        Task<IEnumerable<PedidoDTO>> GetAllPedidos();
        Task UpdateStatusPedido(PedidoDTO pedidoDto);
        Task<PedidoDetaisDTO> GetPedidoById(int id);
        Task<IEnumerable<PedidoDTO>> GetPedidosByClienteId(int clienteId);
        Task<IEnumerable<PedidoDTO>> GetPedidosByStatus(string status);



    }
}
