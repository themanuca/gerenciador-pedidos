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
        Task<List<Pedido>> GetAllPedidos();
        Task CreatePedido(Pedido pedido);
        Task<Pedido> GetPedidoById(int id);
        Task<Pedido> UpdateStatusPedido(Pedido pedido);

    }
}
