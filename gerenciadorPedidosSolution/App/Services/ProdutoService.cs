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
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository) {
            _produtoRepository = produtoRepository;
        }
        public async Task<int> CreateProduto(ProdutoDTO produtodto)
        {
            var validatioDTO = ValidateProdutoDTO(produtodto);
            if (validatioDTO.Any())
            {
                throw new ValidationException("Erro de validação ao criar produto.");
            }
            var produto = new Produto
            {
                Nome = produtodto.Nome,
                Descricao = produtodto.Descricao,
                QuantidadeEstoque = produtodto.QuantidadeEstoque,
                Preco = produtodto.Preco,
            };

            return await _produtoRepository.CreateProduto(produto);
        }

        public async Task DeleteProduto(int id)
        {
            if(id == 0)
            {
                throw new ValidationException("Produto não identificado.");
            }

            await _produtoRepository.DeleteProduto(id);

        }

        public async Task<IEnumerable<ProdutoDTO>> GetAllProduto()
        {
           var produtos = await _produtoRepository.GetAllProduto();
            IEnumerable<ProdutoDTO> produtoDTOs = produtos.Select(source => new ProdutoDTO
            {
                Id = source.Id,
                Nome = source.Nome,
                QuantidadeEstoque= source.QuantidadeEstoque,
                Preco = source.Preco,
                Descricao= source.Descricao,
            });

            return produtoDTOs;
        }

        public async Task<ProdutoDTO> GetProdutoById(int id)
        {
            if(id == 0)
            {
                throw new ValidationException("Produto não identificado.");
            }

            var produto = await _produtoRepository.GetProdutoById(id);

            var produtoDto = new ProdutoDTO 
            { 
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                QuantidadeEstoque = produto.QuantidadeEstoque,
            };

            return produtoDto;
        }

        public async Task UpdateProduto(ProdutoDTO produtoDto)
        {
            var validationDto = ValidateProdutoDTO(produtoDto);
            if (validationDto.Any())
            {
                throw new ValidationException("Erro de validação ao atualizar produto.");
            }
            var getProduto = await _produtoRepository.GetProdutoById(produtoDto.Id);
            if(getProduto == null)
            {
                throw new ValidationException("Produto não identicado.");
            }
            var produto = new Produto 
            { 
               Id = produtoDto.Id,
               Nome = produtoDto.Nome,
               Descricao= produtoDto.Descricao,
               Preco = produtoDto.Preco,
               QuantidadeEstoque = produtoDto.QuantidadeEstoque

            };
            await _produtoRepository.UpdateProduto(produto);
        }

        private List<ValidationError> ValidateProdutoDTO(ProdutoDTO produtoDto)
        {
            var errors = new List<ValidationError>();

            if (produtoDto == null)
            {
                errors.Add(new ValidationError { FieldName = "ProdutoDTO", ErrorMessage = "Por favor, preencha os campos." });
                return errors;
            }

            if (string.IsNullOrEmpty(produtoDto.Nome))
            {
                errors.Add(new ValidationError { FieldName = "Nome", ErrorMessage = "O nome  do produto não pode ser nulo ou vazio." });
            }

            if (string.IsNullOrEmpty(produtoDto.Descricao))
            {
                errors.Add(new ValidationError { FieldName = "Descricao", ErrorMessage = "A descricao não pode ser nulo ou vazio." });
            }

            if (produtoDto.Preco <= 0)
            {
                errors.Add(new ValidationError { FieldName = "Preco", ErrorMessage = "O Preco não pode ser nulo ou vazio." });
            }

            return errors;
        }
    }
}
