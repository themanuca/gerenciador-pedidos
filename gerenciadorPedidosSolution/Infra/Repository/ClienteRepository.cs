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
    public class ClienteRepository: IClienteRepository
    {
        private readonly DBContext _dbContext;
        public ClienteRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateCliente(Cliente cliente)
        {
            var sql = @"
                INSERT INTO Cliente (Nome, Email, Telefone, DataCadastro)
                VALUES (@Nome, @Email, @Telefone, @DataCadastro);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            using var connection = _dbContext.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, cliente);
        }

        public async Task DeleteCliente(int clienteId)
        {
            var sql = "DELETE FROM Cliente WHERE Id = @Id";
            using var connection = _dbContext.CreateConnection();
            await connection.QueryAsync<Cliente>(sql, new {Id =  clienteId});
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            var sql = "SELECT * FROM Cliente";
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<Cliente>(sql);
        }

        public async Task<Cliente> GetClienteById(Cliente clienteId)
        {
            var sql = "SELECT * FROM Cliente WHERE Id = @Id";
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { clienteId.Id });
        }

        public async Task UpdateCliente(Cliente cliente)
        {
            var sql = @"
                UPDATE Cliente
                SET
                    Nome = @Nome,
                    Email = @Email,
                    Telefone = @Telefone
                WHERE
                    Id = @Id;";

            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(sql, cliente);
        }
    }
}
