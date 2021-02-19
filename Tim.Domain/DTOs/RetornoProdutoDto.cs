using System;
using System.Collections.Generic;
using System.Text;

namespace Tim.Domain.DTOs
{
    public class RetornoProdutoDto
    {
        public string Descricao { get; set; }
        public DateTime DataEntrega { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

    }
}
