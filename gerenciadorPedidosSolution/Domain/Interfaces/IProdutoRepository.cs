using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task CreateProduto(Produto produto);
        Task UpdateProduto(Produto produto);
        Task DeleteProduto(int id);
        Task<Produto>GetProdutoById(int id);
        Task<List<Produto>> GetAllProduto();
    }
}
