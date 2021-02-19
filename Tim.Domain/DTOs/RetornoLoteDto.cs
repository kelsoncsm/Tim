using System;
using System.Collections.Generic;
using System.Text;

namespace Tim.Domain.DTOs
{
    public class RetornoLoteDto
    {
        public int Id { get; set; }
        public DateTime DataLote { get; set; }
        public decimal ValorTotal { get; set; }
        public int QuantidadeItens { get; set; }
        public DateTime DataEntrega { get; set; }
    }
}
