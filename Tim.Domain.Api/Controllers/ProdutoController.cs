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

namespace Tim.Domain.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private OleDbConnection _olecon;
        private OleDbCommand _oleCmd;
        private string _urlExcel = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = { 0 }; Extended Properties = 'Excel 12.0 Xml;HDR=YES;ReadOnly=False';";


        [Route("Upload")]
        [HttpPost]
        public async Task<IActionResult> UploadDoc([FromForm(Name = "files")] IList<IFormFile> files, [FromServices] ProdutoHandler produtohandler, [FromServices] LoteHandler loteHandler)
        {



            if (files.Count <= 0)
                return new StatusCodeResult(StatusCodes.Status400BadRequest);


            string erros = string.Empty;
            List<string> listaerros = new List<string>();
            int numLinhaErro = 0;
            int qtdItens = 0;
            decimal valorToral = 0;
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

                    #region CamposObrigatorios
                    if (command.DataEntrega == null)
                    {
                        erros += string.Format("Linha numero {0} Data Entrega campo obrigatorio.", numLinhaErro);

                    }
                    else if (command.DataEntrega.Value <= DateTime.Now)
                    {
                        erros += string.Format("Linha numero {0} -  O campo data de entrega não pode ser menor ou igual que o dia atual", numLinhaErro);

                    }

                    if (string.IsNullOrEmpty(command.Descricao))
                    {
                        erros += string.Format("Linha numero {0} - Nome do Produto campo obrigatorio.", numLinhaErro);
                    }
                    else if (command.Descricao.Length > 50)
                    {
                        erros += string.Format("Linha numero {0} - O campo descrição precisa ter o tamanho máximo de 50 caracteres", numLinhaErro);

                    }

                    if (command.Quantidade == null)
                    {
                        erros += string.Format("Linha numero {0} - Quantidade campo obrigatorio.", numLinhaErro);

                    }
                    else if (command.Quantidade <= 0)
                    {
                        erros += string.Format("Linha numero {0} - O campo quantidade tem que ser maior do que zero", numLinhaErro);

                    }
                    else
                    {
                        qtdItens += command.Quantidade.Value;
                    }

                    if (command.ValorUnitario == null)
                    {
                        erros += string.Format("Linha numero {0} -  Valor Unitário campo obrigatorio. ", numLinhaErro);
                    }
                    else if (command.ValorUnitario <= 0)
                    {
                        erros += string.Format("Linha numero {0} -  O campo Valor Unitário deve ser  maior do que zero", numLinhaErro);

                    }
                    else
                    {
                        command.ValorUnitario = Math.Round(command.ValorUnitario.Value, 2);
                        valorToral += (command.ValorUnitario.Value * command.Quantidade.Value);
                    }

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


                    var retorno = (GenericCommandResult)loteHandler.Handle(new CreateLoteCommand() { DataLote = DateTime.Now, QuantidadeItens = qtdItens, ValorTotal = valorToral });

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
        public async Task<IActionResult> GetById([FromRoute] int id, [FromServices] IProdutoRepository repository)
        {

            try
            {

                Produto retorno = repository.GetById(id);

                return new JsonResult(retorno);
            }
            catch (Exception ex)
            {

                return new JsonResult(new Retorno() { Status = StatusCodes.Status404NotFound, Mensagem = ex.Message });


            }

            //return new JsonResult(await _holderQueryHandler.GetArchiveByHolder(id));
        }

        [Route("")]
        [HttpGet()]
        public IEnumerable<RetornoLoteDto> GetAll([FromServices] IProdutoRepository repository)
        {

            return repository.GetAll();
        }


    }
}
