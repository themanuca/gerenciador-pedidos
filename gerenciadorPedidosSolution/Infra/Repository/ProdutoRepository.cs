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
    public class ProdutoRepository: IProdutoRepository
    {
        private readonly DBContext _dbContext;
        
        public ProdutoRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateProduto(Produto produto)
        {
            var sql = @"
            INSERT INTO Produto (Nome, Descricao, Preco, QuantidadeEstoque)
            VALUES (@Nome, @Descricao, @Preco, @QuantidadeEstoque);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            using var connection = _dbContext.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, produto);
        }

        public async Task DeleteProduto(int id)
        {
            var sql = "DELETE FROM Produto WHERE Id = @Id";
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Produto>> GetAllProduto()
        {
            var sql = "SELECT * FROM Produto";
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Produto>(sql);
        }

        public async Task<Produto> GetProdutoById(int id)
        {
            var sql = "SELECT * FROM Produto WHERE Id = @Id";
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Produto>(sql, new { Id = id });
        }

        public async Task UpdateProduto(Produto produto)
        {
            var sql = @"
            UPDATE Produto
            SET
                Nome = @Nome,
                Descricao = @Descricao,
                Preco = @Preco,
                QuantidadeEstoque = @QuantidadeEstoque
            WHERE
                Id = @Id;";

            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(sql, produto);
        }
    }
}
