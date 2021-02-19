
using System;
using System.Collections.Generic;

namespace Tim.Domain.Entities
{
    public class Produto : Entity
    {




        public Produto(int idLote,string descricao, DateTime dataEntrega, int quantidade, decimal valorUnitario, int id = 0) : base(id)
        {
            Descricao = descricao;
            DataEntrega = dataEntrega;
            Quantidade = quantidade;
            IdLote = idLote;
        }

        public string Descricao { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public Lote Lote { get; private set; }

        public int IdLote { get; private set; }

    }
}