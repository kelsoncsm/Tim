using System;
using System.Collections.Generic;

using System.Text;
using Tim.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Tim.Domain.Infra
{
    public class DBProduto : DbContext
    {

        public DbSet<Produto> Produto { get; set; }

        public DbSet<Lote> Lote { get; set; }

        public DBProduto(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBProduto).Assembly);


      

    }

}