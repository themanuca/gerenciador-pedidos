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
    public class ClienteRepository: IClienteRepository
    {
        private readonly DBContext _dbContext;
        public ClienteRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CreateCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCliente(int clienteId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cliente>> GetAllProduto()
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> GetClienteById(Cliente clienteId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
