using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Entities;
using Data.Interfaces.CoreInterfaces;
using Services.Interfaces.CoreInterfaces;

namespace Services.Interfaces
{
    public interface ITaskService: IRepository<Data.Entities.Task>
    {
        public new IQueryable<Data.Entities.Task> GetAll();
        public Data.Entities.Task GetById(long id);
        public new void AddEntity(Data.Entities.Task entity);
        public new void UpdateEntity(Data.Entities.Task current, Data.Entities.Task update);
        public new void DeleteEntity(Data.Entities.Task entity);
    }
}