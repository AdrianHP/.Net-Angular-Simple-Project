using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.DTOs.EntityDTO;
using Data.Entities;
using Data.Interfaces;
using Services.Interfaces.CoreInterfaces;

namespace Services.Interfaces
{
    public interface ITaskService
    {

        Task<List<TaskDTO>> GetUserTask(string userName, CancellationToken cancellationToken);
    }
}