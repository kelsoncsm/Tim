using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tim.Domain.Api.Util
{
    public class Retorno 
    {
        public int Status { get; set; }
        public List<string> Erros { get; set; }


        public string Mensagem { get; set; }

    }
}
