using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tim.Domain.Api.Util
{
    public class ValidaCampo
    {
        public string ValorMaiorQueZero(string campo, decimal? valor, int linha)
        {
            string retorno = "";

            if (valor == null)
            {
                retorno += string.Format("Linha numero {0} - {1} campo obrigatorio.", campo, linha);

            }
            else if (valor <= 0)
            {
                retorno += string.Format("Linha numero {0} - o Campo {1} tem que ser maior do que zero", campo, linha);

            }

            return retorno;
        }


        public string ValidaCampoVazio(string valor, int linha)
        {
            string retorno = "";

            if (string.IsNullOrEmpty(valor))
            {
                retorno += string.Format("Linha numero {0} - Nome do Produto é campo obrigatorio.", linha);
            }
            else if (valor.Length > 50)
            {
                retorno += string.Format("Linha numero {0} - O nome do Produto precisa ter o tamanho máximo de 50 caracteres", linha);
            }


            return retorno;


        }

        public string ValidaDataMaiorQueHoje(DateTime? data, int linha)
        {

            string retorno = "";


            if (data == null)
            {
                retorno += string.Format("Linha numero {0} Data Entrega campo obrigatorio.", linha);

            }
            else if (data.Value <= DateTime.Now)
            {
                retorno += string.Format("Linha numero {0} -  O campo data de entrega não pode ser menor ou igual que o dia atual", linha);

            }

            return retorno;

        }



    }
}
