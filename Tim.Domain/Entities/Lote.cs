
using System;
using System.Collections.Generic;

namespace Tim.Domain.Entities
{
    public class Lote : Entity
    {

        public Lote(DateTime dataLote, decimal valorTotal, int quantidadeItens, int id = 0) : base(id)
        {
            DataLote = dataLote;
            QuantidadeItens = quantidadeItens;
            ValorTotal = valorTotal;
        }


        public DateTime DataLote{ get; private set; }
        public int QuantidadeItens{ get; private set; }
        public decimal ValorTotal{ get; private set; }


    }
}