using Dapper;
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
    public class PedidoItemRepository : IPedidoItemRepository
    {
        private readonly DBContext _dBContext;
        public PedidoItemRepository(DBContext dbContext)
        {
            _dBContext = dbContext;
        }
        public async Task CreatePedidoItem(PedidoItem pedidoItem)
        {
            var sql = @"INSERT INTO PedidoItem (PedidoId, ProdutoId, Quantidade, PrecoUnitario)
                        VALUES(@PedidoId, @ProdutoId, @Quantidade, @PrecoUnitario);";
            using var connection = _dBContext.CreateConnection();
            await connection.ExecuteScalarAsync(sql, new {
                pedidoItem.PedidoId, 
                pedidoItem.ProdutoId, 
                pedidoItem.Quantidade, 
                pedidoItem.PrecoUnitario
            });
        }

        public async Task<IEnumerable<PedidoItem>> GetPedidoItems(int idPedido)
        {
            var sql = @"SELECT * FROM PedidoItem WHERE PedidoId = @Id;";
            using var connection = _dBContext.CreateConnection();
            
            return await connection.QueryAsync<PedidoItem>(sql, new { Id = idPedido});
        }
        public async Task<IEnumerable<PedidoItemDetais>> GetPedidoItemsWithProduto(int idPedido)
        {
            var sql = @"SELECT 
                        PedidoItem.Id AS Id,
                        PedidoItem.PedidoId,
                        PedidoItem.ProdutoId,
                        PedidoItem.Quantidade,
                        PedidoItem.PrecoUnitario,
                        Produto.Nome AS Nome 
                        FROM PedidoItem 
                        INNER JOIN Produto ON 
                        PedidoItem.ProdutoId = Produto.Id 
                        WHERE PedidoId = @Id;";
            using var connection = _dBContext.CreateConnection();

            return await connection.QueryAsync<PedidoItemDetais>(sql, new { Id = idPedido });
        }
    }
}
