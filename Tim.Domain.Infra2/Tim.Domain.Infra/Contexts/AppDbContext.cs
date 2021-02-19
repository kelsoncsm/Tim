using System;
using Tim.Domain.Entities;
using Tim.Domain.Infra.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;

namespace Tim.Domain.Infra.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produto { get; set; }

        private IDbContextTransaction _currentTransaction;


      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            // _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            _currentTransaction = await Database.BeginTransactionAsync();

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(true);
        }


    }

}
