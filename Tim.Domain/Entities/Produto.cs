
using System;
using System.Collections.Generic;

namespace Tim.Domain.Entities
{
    public class Produto : Entity
    {

       


        public Produto(string descricao, DateTime dataEntrega, int quantidade, decimal valorUnitario, int id = 0) : base(id)
    {
            Descricao = descricao;
            DataEntrega = dataEntrega;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
    }

    public string Descricao { get; set; }
    public DateTime DataEntrega { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }


    }
}