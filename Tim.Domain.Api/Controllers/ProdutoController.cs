using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Threading.Tasks;
using Tim.Domain.Api.Util;
using Tim.Domain.Commands;
using Tim.Domain.DTOs;
using Tim.Domain.Entities;
using Tim.Domain.Handlers;
using Tim.Domain.Repositories;
using Tim.Domain.Api.Util;

namespace Tim.Domain.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private OleDbConnection _olecon;
        private OleDbCommand _oleCmd;
        private string _urlExcel = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = { 0 }; Extended Properties = 'Excel 12.0 Xml;HDR=YES;ReadOnly=False';";
        private ValidaCampo _validaCampo;



        [Route("Upload")]
        [HttpPost]
        public async Task<IActionResult> UploadDoc([FromForm(Name = "files")] IList<IFormFile> files, [FromServices] ProdutoHandler produtohandler, [FromServices] LoteHandler loteHandler)
        {

            if (files.Count <= 0)
                return new StatusCodeResult(StatusCodes.Status400BadRequest);


            string erros = string.Empty;
            List<string> listaerros = new List<string>();
            int numLinhaErro = 0;
            _validaCampo = new ValidaCampo();

            var filePath = string.Empty;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            try
            {
                string _StringConexao = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES;ReadOnly=False';", filePath);
                _olecon = new OleDbConnection(_StringConexao);
                _olecon.Open();


                _oleCmd = new OleDbCommand();
                _oleCmd.Connection = _olecon;
                _oleCmd.CommandType = CommandType.Text;


                _oleCmd.CommandText = "SELECT * FROM [Planilha1$]";
                OleDbDataReader reader = _oleCmd.ExecuteReader();

                List<CreateProdutoCommand> listaInsert = new List<CreateProdutoCommand>();

                CreateProdutoCommand command = new CreateProdutoCommand();
                CreateLoteCommand commandLote = new CreateLoteCommand();
                while (reader.Read())
                {
                    numLinhaErro++;
                    command.DataEntrega = reader["Data Entrega"] != null ? Convert.ToDateTime(reader["Data Entrega"].ToString()) : null;
                    command.Descricao = reader["Nome do Produto"] != null ? reader["Nome do Produto"].ToString() : null;
                    command.Quantidade = reader["Quantidade"] != null ? Convert.ToInt32(reader["Quantidade"].ToString()) : null;
                    command.ValorUnitario = reader["Valor Unitário"] != null ? Convert.ToDecimal(reader["Valor Unitário"].ToString()) : null;

                    #region Validações de campo

                    erros += _validaCampo.ValidaDataMaiorQueHoje(command.DataEntrega, numLinhaErro);
                    erros += _validaCampo.ValidaCampoVazio(command.Descricao, numLinhaErro);
                    erros += _validaCampo.ValorMaiorQueZero("Quantidade", command.Quantidade, numLinhaErro);
                    erros += _validaCampo.ValorMaiorQueZero("Valor Unitário", command.ValorUnitario, numLinhaErro);


                    #endregion

                    if (!string.IsNullOrEmpty(erros))
                    {
                        erros = erros.Substring(0, erros.Length - 1);
                        listaerros.Add(erros);
                        erros = "";
                    }
                    else
                    {
                        listaInsert.Add(command);
                    }

                    command = new CreateProdutoCommand();
                }

                reader.Close();

                if (listaerros.Count != 0)
                {
                    throw new Exception();
                }
                else
                {


                    var retorno = (GenericCommandResult)loteHandler.Handle(new CreateLoteCommand() { DataLote = DateTime.Now });

                    foreach (var item in listaInsert)
                    {
                        item.IdLote = (int)retorno.Data;
                        produtohandler.Handle(item);
                    }

                }


                return new JsonResult(new Retorno() { Status = StatusCodes.Status200OK, Mensagem = "Lote salvo com sucesso" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Retorno() { Status = StatusCodes.Status400BadRequest, Erros = listaerros, Mensagem = "Lote com erros" });



            }
        }



        [Route("{id}")]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromServices] IProdutoRepository repository)
        {

            try
            {
                IEnumerable<RetornoProdutoDto> lista = repository.GetImportById(id);

                return new JsonResult(new { Lista = lista });


            }
            catch (Exception ex)
            {

                return new JsonResult(new Retorno() { Status = StatusCodes.Status400BadRequest, Mensagem = ex.Message });


                //return new JsonResult(new Retorno() { Status = StatusCodes.Status404NotFound, Mensagem = ex.Message });

            }

        }

        [Route("")]
        [HttpGet()]
        public IEnumerable<RetornoLoteDto> GetAll([FromServices] IProdutoRepository repository)
        {

            return repository.GetAllImports();
        }




    }
}
