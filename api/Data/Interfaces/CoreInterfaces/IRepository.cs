using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHealth.Core.Interfaces.CoreInterfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> GetAll();
        public TEntity GetById(object Id);
        public void AddEntity(TEntity entity);
        public void UpdateEntity(TEntity current, TEntity update);
        public void DeleteEntity(TEntity entity);
    }

    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        public IQueryable<TEntity> GetAll();
        public TEntity GetById(TKey Id);
        public void AddEntity(TEntity entity);
        public void UpdateEntity(TEntity current, TEntity update);
        public void DeleteEntity(TEntity entity);
    }
}
