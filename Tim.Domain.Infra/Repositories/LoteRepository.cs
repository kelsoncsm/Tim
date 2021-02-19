using System.Collections.Generic;
using Tim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Tim.Domain.Repositories;


namespace Tim.Domain.Infra.Repositories
{
    public class LoteRepository : ILoteRepository
    {
        private readonly DBProduto _context;

        public LoteRepository(DBProduto context)
        {
            _context = context;
        }

        public void Create(Lote lote)
        {
            _context.Lote.Add(lote);
            _context.SaveChanges();
        }

    }
}