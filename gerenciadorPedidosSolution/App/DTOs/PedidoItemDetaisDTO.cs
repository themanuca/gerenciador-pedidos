using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTOs
{
    public class PedidoItemDetaisDTO
    {
        public PedidoDTO Pedido { get; set; }
        public List<ProdutoDTO> Produtos { get; set; } 
    }
}
