﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<int>CreateCliente(Cliente cliente);
        Task UpdateCliente(Cliente cliente);
        Task DeleteCliente(int clienteId);
        Task<Cliente> GetClienteById(Cliente clienteId);
        Task<IEnumerable<Cliente>> GetAllClientes();
    }
}
