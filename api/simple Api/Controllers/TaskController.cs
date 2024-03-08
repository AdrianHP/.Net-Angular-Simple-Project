using Data.DTOs.EntityDTO;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.WebUtilities;
using Services.Interfaces.CoreInterfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : DataControllerBase<Data.Entities.Task,TaskDTO, int>
    {
        public TaskController(IDataCRUDService<IDataContext> dataService, ICrudHandler<Data.Entities.Task, TaskDTO, int> dataHandler = null)
            : base(dataService, dataHandler)
        {
        }
    }
}
