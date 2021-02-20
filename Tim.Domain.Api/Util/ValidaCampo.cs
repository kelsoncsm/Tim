using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tim.Domain.Commands;

namespace Tim.Domain.Api.Util
{
    public class Validar
    {
        public string valorMaiorQueZero(string campo, decimal? valor, int linha)
        {
            string retorno = "";

            if (valor == null)
            {
                retorno += string.Format("Linha {0} - {1} Campo obrigatorio.", campo, linha);

            }
            else if (valor <= 0)
            {
                retorno += string.Format("Linha {0} - Campo {1} tem que ser maior do que zero", campo, linha);

            }

            return retorno;
        }


        public string validaCampoVazio(string valor, int linha)
        {
            string retorno = "";

            if (string.IsNullOrEmpty(valor))
            {
                retorno += string.Format("Linha {0} - Nome do Produto é campo obrigatorio.", linha);
            }
            else if (valor.Length > 50)
            {
                retorno += string.Format("Linha {0} - O nome do Produto precisa ter o tamanho máximo de 50 caracteres", linha);
            }


            return retorno;


        }

        public string validaDataMaiorQueHoje(DateTime? data, int linha)
        {

            string retorno = "";


            if (data == null)
            {
                retorno += string.Format("Linha {0} - Data Entrega campo obrigatorio.", linha);

            }
            else if (data.Value <= DateTime.Now)
            {
                retorno += string.Format("Linha {0} - O campo data de entrega não pode ser menor ou igual que o dia atual", linha);

            }

            return retorno;

        }



        public List<string> ListaDados (List<CreateProdutoCommand> _lista)
        {
            List<string> listaerros = new List<string>();
            int numLinhaErro = 0;
            string erros = string.Empty;
            foreach (var item in _lista)
            {

                numLinhaErro++;
                #region Validações de campo

                erros += validaDataMaiorQueHoje(item.DataEntrega, numLinhaErro);
                erros += validaCampoVazio(item.Descricao, numLinhaErro);
                erros += valorMaiorQueZero("Quantidade", item.Quantidade, numLinhaErro);
                erros += valorMaiorQueZero("Valor Unitário", item.ValorUnitario, numLinhaErro);

                #endregion

                if (!string.IsNullOrEmpty(erros))
                {
                    erros = erros.Substring(0, erros.Length - 1);
                    listaerros.Add(erros);
                    erros = "";
                }

            }


            return listaerros;
           

        }



    }
}
