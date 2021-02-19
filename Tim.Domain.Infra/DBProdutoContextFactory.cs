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

     
        public DBProduto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBProduto>();
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-NLBJUPA4\\SQLEXPRESS; Initial Catalog=db-tim; Integrated Security=True");
            return new DBProduto(optionsBuilder.Options);
        }
    }


}