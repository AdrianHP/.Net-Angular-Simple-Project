using Data.DTOs.EntityDTO;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.WebUtilities;
using Services.Interfaces;
using Services.Interfaces.CoreInterfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TaskController : DataControllerBase<Data.Entities.Task,TaskDTO, int>
    {
        private readonly ITaskService _taskService;
        public TaskController(IDataCRUDService<IDataContext> dataService, ITaskService taskService, ICrudHandler<Data.Entities.Task, TaskDTO, int> dataHandler = null)
            : base(dataService, dataHandler)
        {
          _taskService = taskService;
        }

        [HttpGet]
        [Route("getUSerTasks")]
        public async Task<IActionResult> GetUserTasks(CancellationToken cancellationToken = default)
        {
            var userName = HttpContext.User.Identity.Name;
            return Ok(await _taskService.GetUserTask(userName, cancellationToken));
        }
    }
}
