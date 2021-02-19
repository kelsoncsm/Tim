using System;
using Tim.Domain.Commands.Contracts;
using Tim.Domain.Util;

namespace Tim.Domain.Commands
{
    public class CreateLoteCommand : Notifiable, ICommand
    {
        public CreateLoteCommand()
        {
        }

        public CreateLoteCommand(DateTime dataLote, decimal valorTotal, int quantidadeItens, int id = 0)
        {
            DataLote = dataLote;
            QuantidadeItens = quantidadeItens;
            ValorTotal = valorTotal;
        }


        public DateTime DataLote { get; private set; }
        public int QuantidadeItens { get; private set; }
        public decimal ValorTotal { get; private set; }



        public void Validate()
        {
           
        }
    }
}