using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class PedidoRepository: IPedidoRepository
    {
        private readonly DBContext _dbContext;

        public PedidoRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CreatePedido(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pedido>> GetAllPedidos()
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> GetPedidoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> UpdateStatusPedido(Pedido pedido)
        {
            throw new NotImplementedException();
        }
    }
}
