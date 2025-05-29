using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DTOs
{
    public class PedidoDetaisDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; }
        public List<PedidoItemDetaisDTO> ItensPedido { get; set; } = new List<PedidoItemDetaisDTO>();
    }
}
