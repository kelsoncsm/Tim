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


        public IEnumerable<RetornoLoteDto> GetAllImports()
        {


            var query = (from p in _context.Produto
                         join l in _context.Lote
                         on p.IdLote equals l.Id
                         group new { p, l } by new { l.Id,l.DataLote } into g
                         select new RetornoLoteDto
                         {
                             Id = g.Key.Id,
                             QuantidadeItens = g.Sum(x=>x.p.Quantidade),
                             ValorTotal = g.Sum(x=>x.p.ValorUnitario),
                             DataLote = g.Key.DataLote,
                             DataEntrega = g.Min(x => x.p.DataEntrega)
                         });

            return query;

        }

     
        public IEnumerable<RetornoProdutoDto> GetImportById(int id)
        {


            var query = (from p in _context.Produto
                         join l in _context.Lote
                        on p.IdLote equals l.Id
                        where l.Id == id
                         group new { p } by new { p.Quantidade, p.ValorUnitario, p.Descricao, p.DataEntrega } into g
                         select new RetornoProdutoDto
                         {
                             DataEntrega = g.Min(x => x.p.DataEntrega),
                             Descricao = g.Key.Descricao,
                             Quantidade = g.Key.Quantidade,
                             ValorUnitario = g.Key.ValorUnitario,
                             ValorTotal = g.Key.ValorUnitario * g.Key.Quantidade
                         });

            if (query.ToList().Count == 0)
                throw new Exception("Registro não encontrado");



            return query;

        }


    }
}