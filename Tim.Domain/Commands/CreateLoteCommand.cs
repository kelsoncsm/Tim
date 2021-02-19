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

        public CreateLoteCommand(DateTime dataLote, int id = 0)
        {
            DataLote = dataLote;
        }


        public DateTime DataLote { get;  set; }
      
        public void Validate()
        {

        }
    }
}