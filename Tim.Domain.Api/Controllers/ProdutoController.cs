using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tim.Domain.Api.Util;
using Tim.Domain.Commands;
using Tim.Domain.DTOs;
using Tim.Domain.Handlers;
using Tim.Domain.Repositories;

namespace Tim.Domain.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {

        private LerExcel _lerExel;
        private Validar _validar;

        [Route("Upload")]
        [HttpPost]
        public async Task<IActionResult> UploadDoc([FromForm(Name = "files")] IList<IFormFile> files, [FromServices] ProdutoHandler produtohandler, [FromServices] LoteHandler loteHandler)
        {

            if (files.Count <= 0)
                return new StatusCodeResult(StatusCodes.Status400BadRequest);

           
            List<string> listaerros = new List<string>();
            _lerExel = new LerExcel();
            _validar = new Validar();

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

                List<CreateProdutoCommand> lista = _lerExel.RetornaLista(filePath);

                if (lista.Count > 0)
                    listaerros = _validar.ListaDados(lista);

                if (listaerros.Count != 0)
                {
                    throw new Exception("Lote com erros");
                }
                else
                {

                    var retorno = (GenericCommandResult)loteHandler.Handle(new CreateLoteCommand() { DataLote = DateTime.Now });

                    foreach (var item in lista)
                    {
                        item.IdLote = (int)retorno.Data;
                        produtohandler.Handle(item);
                    }

                }


                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new JsonResult(new Retorno() { Status = StatusCodes.Status400BadRequest, Erros = listaerros, Mensagem = ex.Message});


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
