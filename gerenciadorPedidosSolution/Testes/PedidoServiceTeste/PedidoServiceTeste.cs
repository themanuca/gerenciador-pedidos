using App.DTOs;
using App.Interfaces;
using App.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Testes.PedidoServiceTeste
{
    public class PedidoServiceTeste
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly Mock<IPedidoItemRepository> _mockPedidoItemRepository;
        private readonly PedidoService _pedidoService;

        public PedidoServiceTeste()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _mockPedidoItemRepository = new Mock<IPedidoItemRepository>();
            _pedidoService = new PedidoService(
                _pedidoRepositoryMock.Object,
                _produtoRepositoryMock.Object,
                _mockPedidoItemRepository.Object
            );
        }

        [Fact]
        public async Task CreatePedido_ValidInput_ReturnsPedidoDTO()
        {
            // Arrange
            var pedidoDto = new PedidoDTO
            {
                ClienteId = 1,
                ItensPedido = new List<PedidoItem>
                {
                    new PedidoItem { ProdutoId = 1, Quantidade = 2 }
                }
            };
            var produto = new Produto { Id = 1, Nome = "Produto 1", Preco = 10m, QuantidadeEstoque = 5 };
            _produtoRepositoryMock.Setup(repo => repo.GetProdutoById(1)).ReturnsAsync(produto);
            _pedidoRepositoryMock.Setup(repo => repo.CreatePedido(It.IsAny<Pedido>())).ReturnsAsync(1);
            _produtoRepositoryMock.Setup(repo => repo.UpdateProduto(It.IsAny<Produto>())).Returns(Task.CompletedTask);

            // Act
            var result = await _pedidoService.CreatePedido(pedidoDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.ClienteId.Should().Be(1);
            result.ValorTotal.Should().Be(20m);
            result.Status.Should().Be("Novo");
            result.DataPedido.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
            result.ItensPedido.Should().HaveCount(1);
            _produtoRepositoryMock.Verify(repo => repo.UpdateProduto(It.Is<Produto>(p => p.Id == 1 && p.QuantidadeEstoque == 3)), Times.Once());
        }

        [Fact]
        public async Task CreatePedido_MultipleItems_UpdatesStockCorrectly()
        {
            // Arrange
            var pedidoDto = new PedidoDTO
            {
                ClienteId = 1,
                ItensPedido = new List<PedidoItem>
                {
                    new PedidoItem { ProdutoId = 1, Quantidade = 2 },
                    new PedidoItem { ProdutoId = 2, Quantidade = 3 }
                }
            };
            var produto1 = new Produto { Id = 1, Nome = "Produto 1", Preco = 10m, QuantidadeEstoque = 5 };
            var produto2 = new Produto { Id = 2, Nome = "Produto 2", Preco = 20m, QuantidadeEstoque = 10 };
            _produtoRepositoryMock.Setup(repo => repo.GetProdutoById(1)).ReturnsAsync(produto1);
            _produtoRepositoryMock.Setup(repo => repo.GetProdutoById(2)).ReturnsAsync(produto2);
            _pedidoRepositoryMock.Setup(repo => repo.CreatePedido(It.IsAny<Pedido>())).ReturnsAsync(1);
            _produtoRepositoryMock.Setup(repo => repo.UpdateProduto(It.IsAny<Produto>())).Returns(Task.CompletedTask);

            // Act
            var result = await _pedidoService.CreatePedido(pedidoDto);

            // Assert
            result.ValorTotal.Should().Be(80m);
            result.ItensPedido.Should().HaveCount(2);
            _produtoRepositoryMock.Verify(repo => repo.UpdateProduto(It.Is<Produto>(p => p.Id == 1 && p.QuantidadeEstoque == 3)), Times.Once());
            _produtoRepositoryMock.Verify(repo => repo.UpdateProduto(It.Is<Produto>(p => p.Id == 2 && p.QuantidadeEstoque == 7)), Times.Once());
        }

        [Fact]
        public async Task CreatePedido_NullPedidoDto_ThrowsArgumentException()
        {
            // Arrange
            PedidoDTO pedidoDto = null;

            // Act
            Func<Task> act = async () => await _pedidoService.CreatePedido(pedidoDto);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Dados do pedido inválidos.");
        }
    }
}
