using System;
using Tim.Domain.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Tim.Domain.Infra
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<Contexts.AppDbContext>())
                {
                    try
                    {
                        RelationalDatabaseFacadeExtensions.Migrate(appContext.Database);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            return host;
        }
    }
}