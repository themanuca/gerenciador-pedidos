using App.DTOs;
using App.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoItemRepository _itemRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository, IPedidoItemRepository pedidoItemRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _itemRepository = pedidoItemRepository;
        }
        public async Task<PedidoDTO> CreatePedido(PedidoDTO pedidoDto)
        {
            if (pedidoDto == null || pedidoDto.ClienteId <= 0 || pedidoDto.ItensPedido == null || !pedidoDto.ItensPedido.Any())
            {
                throw new ArgumentException("Dados do pedido inválidos.");
            }
            decimal valorTotal = 0;
            foreach (var itemDto in pedidoDto.ItensPedido)
            {
                var produto = await _produtoRepository.GetProdutoById(itemDto.ProdutoId);
                if (produto == null)
                {
                    throw new ArgumentException($"Produto {itemDto.ProdutoId} não encontrado.");
                }
                if (produto.QuantidadeEstoque < itemDto.Quantidade)
                {
                    throw new InvalidOperationException($"Estoque insuficiente para o produto {produto.Nome}.");
                }
                valorTotal += itemDto.Quantidade * produto.Preco;
            }

            var pedido = new Pedido
            {
                ClienteId = pedidoDto.ClienteId,
                ItensPedido = pedidoDto.ItensPedido

            };//_mapper.Map<Pedido>(pedidoDto);
            pedido.DataPedido = DateTime.Now;
            pedido.Status = "Novo";
            pedido.ValorTotal = valorTotal;

            var pedidoId = await _pedidoRepository.CreatePedido(pedido);
            pedido.Id = pedidoId;

            foreach (var itemDto in pedido.ItensPedido)
            {
                itemDto.PedidoId = pedidoId;
                itemDto.PrecoUnitario = pedido.ValorTotal / itemDto.Quantidade;
                await _itemRepository.CreatePedidoItem(itemDto);

                var produto = await _produtoRepository.GetProdutoById(itemDto.ProdutoId);
                produto.QuantidadeEstoque -= itemDto.Quantidade;
                await _produtoRepository.UpdateProduto(produto); 
            }

            return new PedidoDTO { 
                Id = pedido.Id,
                ClienteId = pedido.ClienteId,
                DataPedido = pedido.DataPedido,
                Status = pedido.Status,
                ValorTotal = pedido.ValorTotal,
                ItensPedido = pedido.ItensPedido
            };
        }

        public async Task<IEnumerable<PedidoDTO>> GetAllPedidos()
        {
            var pedidos = await _pedidoRepository.GetAllPedidos();
            var pedidosComDetalhes = new List<PedidoItemDetaisDTO>();
            IEnumerable<PedidoDTO> pedidoDTOs = pedidos.Select(source => new PedidoDTO { 
              Id=source.Id,
              ClienteId=source.ClienteId,
              DataPedido=source.DataPedido,
              Status = source.Status,
              ValorTotal = source.ValorTotal,
              ItensPedido = source.ItensPedido
            });

            return pedidoDTOs;
        }

        public async Task<PedidoDetaisDTO> GetPedidoById(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoById(id);
            if(pedido == null)
            {
                throw new ArgumentException("Pedido não encontrado.");
            }
            
            var pedidoItemResult = await _itemRepository.GetPedidoItemsWithProduto(pedido.Id) ?? throw new ArgumentException("Sem itens adicionados.");
            IEnumerable<PedidoItemDetaisDTO> pedidoItensDetails = pedidoItemResult.Select(source => new PedidoItemDetaisDTO {
                Id = source.Id,
                Nome = source.Nome,
                PedidoId = source.PedidoId,
                PrecoUnitario = source.PrecoUnitario,
                ProdutoId = source.ProdutoId,
                Quantidade = source.Quantidade,
            });

            return new PedidoDetaisDTO
            { 
                Id = pedido.Id,
                ClienteId=pedido.ClienteId,
                DataPedido = pedido.DataPedido,
                Status= pedido.Status,
                ValorTotal= pedido.ValorTotal,
                ItensPedido = pedidoItensDetails
            };
        }

        public async Task<IEnumerable<PedidoDTO>> GetPedidosByClienteId(int clienteId)
        {
            var pedidos = await _pedidoRepository.GetPedidosByClienteId(clienteId);
            IEnumerable<PedidoDTO> pedidoDTOs = pedidos.Select(source => new PedidoDTO
            {
                Id = source.Id,
                ClienteId = source.ClienteId,
                DataPedido = source.DataPedido,
                Status = source.Status,
                ValorTotal = source.ValorTotal,
                ItensPedido = source.ItensPedido
            });

            return pedidoDTOs;
        }

        public async Task<IEnumerable<PedidoDTO>> GetPedidosByStatus(string status)
        {
            var pedidos = await _pedidoRepository.GetPedidosByStatus(status);
            IEnumerable<PedidoDTO> pedidoDTOs = pedidos.Select(source => new PedidoDTO
            {
                Id = source.Id,
                ClienteId = source.ClienteId,
                DataPedido = source.DataPedido,
                Status = source.Status,
                ValorTotal = source.ValorTotal,
                ItensPedido = source.ItensPedido
            });

            return pedidoDTOs;
        }

        public async Task UpdateStatusPedido(PedidoDTO pedidoDto)
        {
            var pedidoExistente = await _pedidoRepository.GetPedidoById(pedidoDto.Id);
            if (pedidoExistente == null)
            {
                throw new KeyNotFoundException($"Pedido com ID {pedidoDto.Id} não encontrado.");
            }

            var pedido = new Pedido { 
                Id = pedidoDto.Id,
                Status = pedidoDto.Status
            };
            await _pedidoRepository.UpdateStatusPedido(pedido);
        }
    }
}
