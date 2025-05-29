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
    public class PedidoRepository: IPedidoRepository
    {
        private readonly DBContext _dbContext;

        public PedidoRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreatePedido(Pedido pedido)
        {
            var sql = @"INSERT INTO Pedido (ClienteId, DataPedido, ValorTotal, Status)
                        VALUES (@ClienteId, @DataPedido, @ValorTotal, @Status)
                        SELECT CAST(SCOPE_IDENTITY() as int);";
            using var connection = _dbContext.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, new {
                ClienteId = pedido.ClienteId,
                DataPedido = pedido.DataPedido,
                ValorTotal = pedido.ValorTotal,
                Status = pedido.Status
            });
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidos()
        {
            var sql = "SELECT * FROM Pedido;";
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Pedido>(sql);
        }

        public async Task<Pedido> GetPedidoById(int id)
        {
            var sql = @"SELECT
                        P.Id,
                        P.ClienteId,
                        P.DataPedido,
                        P.ValorTotal,
                        P.Status,
                        IP.Id AS ItemPedidoId,
                        IP.ProdutoId,
                        IP.Quantidade,
                        IP.PrecoUnitario
                    FROM
                        Pedido P
                    INNER JOIN
                        PedidoItem IP ON P.Id = IP.PedidoId
                    WHERE
                        P.Id = @Id;";
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Pedido>(sql, new { Id = id});
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByClienteId(int clienteId)
        {
            var sql = "SELECT * FROM Pedido WHERE ClienteId = @ClienteId;";

            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Pedido>(sql, new { ClienteId = clienteId });
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByStatus(string status)
        {
            var sql = "SELECT * FROM Pedido WHERE Status = @Status;";

            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Pedido>(sql, new { Status = status });
        }

        public async Task UpdateStatusPedido(Pedido pedido)
        {
            var sql = @"
                UPDATE Pedido
                SET
                    Status = @Status
                WHERE
                    Id = @Id;";
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(sql, new { pedido.Status, pedido.Id });
        }
    }
}
