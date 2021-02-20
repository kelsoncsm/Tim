using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tim.Domain.Commands;
 

namespace Tim.Domain.Api.Util
{
    public class LerExcel 
    {

        private OleDbConnection _olecon;
        private OleDbCommand _oleCmd;
       
       
        public List<CreateProdutoCommand> RetornaLista(string caminho)
        {
            List<CreateProdutoCommand> lista = new List<CreateProdutoCommand>();
            _olecon = new OleDbConnection(String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES;ReadOnly=False';",caminho));
            _olecon.Open();

            _oleCmd = new OleDbCommand();
            _oleCmd.Connection = _olecon;
            _oleCmd.CommandText = "SELECT * FROM [Planilha1$]";

            OleDbDataReader reader = _oleCmd.ExecuteReader();

            while (reader.Read())
            {

                lista.Add(new CreateProdutoCommand()
                {
                   
                    Descricao = reader["Nome do Produto"] != null ? reader["Nome do Produto"].ToString() : "",
                    DataEntrega = reader["Data Entrega"] != null ? Convert.ToDateTime(reader["Data Entrega"].ToString()) : null,
                    Quantidade = reader["Quantidade"] != null ? Convert.ToInt32(reader["Quantidade"].ToString()) : null,
                    ValorUnitario = reader["Valor Unitário"] != null ? Convert.ToDecimal(reader["Valor Unitário"].ToString()) : null

                });
            }
            reader.Close();
            _olecon.Close();
            return lista;

        }

    }
}
