using System;
using Tim.Domain.Commands.Contracts;
using Tim.Domain.Util;

namespace Tim.Domain.Commands
{
    public class CreateProdutoCommand : Notifiable, ICommand
    {
        public CreateProdutoCommand()
        {
        }

        public CreateProdutoCommand(int idLote, string descricao, DateTime dataEntrega, int quantidade, decimal valorUnitario)
        {
            Descricao = descricao;
            DataEntrega = dataEntrega;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            IdLote = idLote;
        }

        public string? Descricao { get; set; }
        public DateTime? DataEntrega { get; set; }
        public int? Quantidade { get; set; }
        public decimal? ValorUnitario { get; set; }
        public int IdLote { get; set; }


        public void Validate()
        {
            //if (string.IsNullOrEmpty(Titulo))
            //  AddNotification("Titulo", "O título deve ser informado");
            //if (string.IsNullOrEmpty(Genero))
            //  AddNotification("Genero", "O genêro deve ser informado");
            //if (string.IsNullOrEmpty(Autor))
            //  AddNotification("Autor", "O Autor deve ser informado");
            //if (string.IsNullOrEmpty(Capa))
            //  AddNotification("Capa", "A capa é obrigatória");

        }
    }
}