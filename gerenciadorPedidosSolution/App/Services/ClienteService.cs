using App.DTOs;
using App.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class ClienteService:IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task CreateCliente(ClienteDTO clienteDto)
        {
            var validation = ValidateClienteDTO(clienteDto);
            if (validation.Any()) 
            {
                throw new ValidationException("Erro de validação ao criar cliente.");
            }

            var cliente = new Cliente
            {
                Nome = clienteDto.Nome,
                Email = clienteDto.Email,
                Telefone = clienteDto.Telefone,
                DataCadastro = DateTime.Now

            };
            await _clienteRepository.CreateCliente(cliente);
        }

        public async Task DeleteCliente(int clienteId)
        {
            if(clienteId == 0)
            {
                throw new ValidationException("Cliente não encontrado.");
            }
            await _clienteRepository.DeleteCliente(clienteId);
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllClientes()
        {
            var clientes = await _clienteRepository.GetAllClientes();
            IEnumerable<ClienteDTO> cliente = clientes.Select(source => new ClienteDTO
            {
                Id = source.Id,
                Nome = source.Nome,
                Email = source.Email,
                Telefone = source.Telefone,
                DataCadastro= source.DataCadastro
            });
            return cliente;
        }

        public async Task<ClienteDTO> GetClienteById(ClienteDTO clienteId)
        {
           if(clienteId == null)
            {
                throw new ValidationException("Cliente não encontrado.");
            }
           
           var cliente = await _clienteRepository.GetClienteById(new Cliente { Id = clienteId.Id });
            return new ClienteDTO
            {
                Id = clienteId.Id,
                Nome= cliente.Nome,
                Telefone = cliente.Telefone,
                Email = cliente.Email,
                DataCadastro = cliente.DataCadastro
            };
        }

        public async Task UpdateCliente(ClienteDTO clienteDto)
        {
            var validation = ValidateClienteDTO(clienteDto);
            if (validation.Any())
            {
                throw new ValidationException("Erro de validação ao criar cliente.");
            }
            var getCliente = await GetClienteById(clienteDto);
            if(getCliente == null)
            {
                throw new ValidationException("Cliente não encontrado.");
            }
            var cliente = new Cliente
            {
                Id = clienteDto.Id,
                Nome = clienteDto.Nome ?? getCliente.Nome,
                Email = clienteDto.Email ?? getCliente.Email,
                Telefone = clienteDto.Telefone ?? getCliente.Telefone,
                DataCadastro = DateTime.Now

            };

            await _clienteRepository.UpdateCliente(cliente);
        }
        private List<ValidationError> ValidateClienteDTO(ClienteDTO clienteDto)
        {
            var errors = new List<ValidationError>();

            if (clienteDto == null)
            {
                errors.Add(new ValidationError { FieldName = "ClienteDTO", ErrorMessage = "Por favor, preencha os campos." });
                return errors;
            }

            if (string.IsNullOrEmpty(clienteDto.Nome))
            {
                errors.Add(new ValidationError { FieldName = "Nome", ErrorMessage = "O nome não pode ser nulo ou vazio." });
            }

            if (string.IsNullOrEmpty(clienteDto.Email))
            {
                errors.Add(new ValidationError { FieldName = "Email", ErrorMessage = "O email não pode ser nulo ou vazio." });
            }

            if (string.IsNullOrEmpty(clienteDto.Telefone))
            {
                errors.Add(new ValidationError { FieldName = "Telefone", ErrorMessage = "O telefone não pode ser nulo ou vazio." });
            }

            return errors;
        }
    }
}
