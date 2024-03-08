using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDataDbContext
    {
        DatabaseFacade Database { get; }

        DbSet<T> Set<T>() where T : class;

        EntityEntry<T> Update<T>(T entity) where T : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity item) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task UpdateManyAsync<TEntity>(List<TEntity> entityItems, CancellationToken cancellationToken) where TEntity : class;

        Task UpdateManyBatchedAsync<TEntity>(IEnumerable<TEntity> items, int batchSize, CancellationToken cancellationToken) where TEntity : class;

        void DetachEntry<TEntity>(TEntity entry) where TEntity : class;

        ValueTask DisposeAsync();
    }
}
