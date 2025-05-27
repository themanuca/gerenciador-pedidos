using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetAllPedidos();
        Task<int>CreatePedido(Pedido pedido);
        Task<Pedido> GetPedidoById(int id);
        Task UpdateStatusPedido(Pedido pedido);
        Task<IEnumerable<Pedido>> GetPedidosByClienteId(int clienteId);
        Task<IEnumerable<Pedido>> GetPedidosByStatus(string status);

    }
}
