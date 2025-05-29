using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPedidoItemRepository
    {
        Task CreatePedidoItem(PedidoItem pedidoItem);
        Task<IEnumerable<PedidoItem>>GetPedidoItems(int idPedido);
        Task<IEnumerable<PedidoItemDetais>> GetPedidoItemsWithProduto(int idPedido);
    }
}
