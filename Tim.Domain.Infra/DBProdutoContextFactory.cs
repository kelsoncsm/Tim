using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tim.Domain.Infra
{
    public class DBProdutoContextFactory : IDesignTimeDbContextFactory<DBProduto>
    {

        public DBProdutoContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        public DBProduto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBProduto>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("connectionString"));
            return new DBProduto(optionsBuilder.Options);
        }
    }


}