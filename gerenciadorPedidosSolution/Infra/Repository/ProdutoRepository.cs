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
    public class ProdutoRepository: IProdutoRepository
    {
        private readonly DBContext _dbContext;
        
        public ProdutoRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CreateProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduto(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Produto>> GetAllProduto()
        {
            throw new NotImplementedException();
        }

        public Task<Produto> GetProdutoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduto(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
