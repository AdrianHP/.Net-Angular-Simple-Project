using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Data.SqlServer
{
   public class DataContext: IdentityDbContext<User, Role, string>,IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        #region Account

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        #endregion

        #region Entities
        public DbSet<Data.Entities.Task> Tasks { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                 .HasMany(e => e.Tasks)
                 .WithOne(e => e.AssignedUser)
                 .HasForeignKey(e => e.AssignedUserId)
                 .IsRequired();
        }

        public async System.Threading.Tasks.Task UpdateManyAsync<TEntity>(List<TEntity> items, CancellationToken cancellationToken) where TEntity : class
        {
            Set<TEntity>().UpdateRange(items);
            await SaveChangesAsync(cancellationToken);
            items.ForEach(DetachEntry);
        }

        public async System.Threading.Tasks.Task UpdateManyBatchedAsync<TEntity>(IEnumerable<TEntity> items, int batchSize, CancellationToken cancellationToken) where TEntity : class
        {
            var itemsList = items.ToList();
            var skip = 0;
            for (var j = 0; j < itemsList.Count; j += batchSize)
            {
                var itemsSubset = itemsList.Skip(skip).Take(batchSize).ToList();
                await UpdateManyAsync(itemsSubset, cancellationToken);

                skip += batchSize;
            }
        }

        public void DetachEntry<TEntity>(TEntity entry) where TEntity : class
        {
            Entry(entry).State = EntityState.Detached;
        }
    }
}
