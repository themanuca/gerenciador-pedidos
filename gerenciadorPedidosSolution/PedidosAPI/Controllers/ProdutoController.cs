using App.DTOs;
using App.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PedidosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var produtos = await _produtoService.GetAllProduto();
                return Ok(produtos);
            }
            catch {

                throw;
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var produto = await _produtoService.GetProdutoById(id);
                return Ok(produto);
            }
            catch {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProdutoDTO produtoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _produtoService.CreateProduto(produtoDto);
                return Ok();
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, ProdutoDTO produtoDto)
        {
            try
            {
                produtoDto.Id = id;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _produtoService.UpdateProduto(produtoDto);
                return Ok();
            }
            catch
            {
                throw;
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _produtoService.DeleteProduto(id);
                return Ok();
            }
            catch {
                throw;
            }
        }
    }
}
