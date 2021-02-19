using System.Collections.Generic;
using Tim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Tim.Domain.Repositories;
using Tim.Domain.Queries;

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


    public IEnumerable<Produto> GetAll()
    {
      var query = _context.Produto
        .AsNoTracking();

     
      return query.OrderBy(x => x.Descricao);
    }

    public Produto GetById(int id)
    {
      return _context
          .Produto
          .Where(x => x.Id == id)
          .FirstOrDefault();
    }

  }
}