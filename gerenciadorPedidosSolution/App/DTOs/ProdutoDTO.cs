using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O preço é obrigatório.")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "A quantidade é obrigatório.")]
        public int QuantidadeEstoque { get; set; }
    }
}
