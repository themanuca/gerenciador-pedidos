using App.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IProdutoService
    {
        Task<int> CreateProduto(ProdutoDTO produto);
        Task UpdateProduto(ProdutoDTO produto);
        Task DeleteProduto(int id);
        Task<ProdutoDTO> GetProdutoById(int id);
        Task<IEnumerable<ProdutoDTO>> GetAllProduto();
    }
}
