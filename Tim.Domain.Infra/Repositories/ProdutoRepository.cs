using System.Collections.Generic;
using Tim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Tim.Domain.Repositories;
using Tim.Domain.DTOs;
using System.Collections;

namespace Tim.Domain.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DBProduto _context;


        public ProdutoRepository(DBProduto context)
        {
            _context = context;
        }

        public void Create(Produto produto)
        {
            _context.Produto.Add(produto);
            _context.SaveChanges();
        }


        public IEnumerable<RetornoLoteDto> GetAll()
        {


            var query = (from p in _context.Produto
                         join l in _context.Lote
                         on p.IdLote equals l.Id
                         group new { p, l } by new { l.Id, l.QuantidadeItens, l.ValorTotal, l.DataLote } into g
                         select new RetornoLoteDto
                         {
                             Id = g.Key.Id,
                             QuantidadeItens = g.Key.QuantidadeItens,
                             ValorTotal = g.Key.ValorTotal,
                             DataLote = g.Key.DataLote,
                             DataEntrega = g.Min(x => x.p.DataEntrega)
                         });

            return query;

        }

        public Produto GetById(int id)
        {

            Produto retorno = _context
             .Produto
             .Where(x => x.Id == id)
             .FirstOrDefault();

            if (retorno == null)
                throw new Exception("Registro não encontrado");


            return retorno;
        }

    }
}