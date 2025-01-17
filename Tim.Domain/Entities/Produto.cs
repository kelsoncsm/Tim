
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
            ValorUnitario = valorUnitario;
            IdLote = idLote;
        }

        public string Descricao { get;  set; }
        public DateTime DataEntrega { get;  set; }
        public int Quantidade { get;  set; }
        public decimal ValorUnitario { get;  set; }
        public Lote Lote { get;  set; }

        public int IdLote { get;  set; }

    }
}