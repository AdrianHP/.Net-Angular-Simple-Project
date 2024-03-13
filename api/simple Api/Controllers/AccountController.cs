using Data.DTOs.AccountDTO;
using Data.Exceptions;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Interfaces;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace Server.Controllers
{
    [Produces("application/json")]
    [Route("api/account"), ResponseCache(NoStore = true)]
    [ApiController]
    public class AccountApiController : WebUtilities.ControllerBase
    {
        ILogger<AccountApiController> _logger;
        private readonly IUserService _userService;
        private readonly IDataContext _dataContext;


        public AccountApiController(
            IUserService userService,
            ILogger<AccountApiController> logger,
            IDataContext dataContext)
        {
            _logger = logger;
            _userService = userService;
            _dataContext = dataContext;
        }

        
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO dto, CancellationToken cancellationToken = default)
        {
            var result = await _userService.Register(dto, cancellationToken);
            _logger.LogInformation($"Registered user: {dto.Email}");
            return result.Succeeded ? StatusCode(201) : new BadRequestObjectResult(result);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto, CancellationToken cancellationToken = default)
        {
            try
            {
                var loggedUser = await this._userService.Login(dto, cancellationToken);
                return  Ok(new { 
                    Token = await this._userService.CreateTokenAsync(),
                    LoggedUser = loggedUser
                });
            }
          
            catch (Exception exception)
            {
                _logger.LogDebug(message: exception.Message);

                
                return BadRequest(exception.Message);
            }
        }



        [HttpGet]
        [AllowAnonymous]
        [Authorize]
        public IActionResult TestGet()
        {
            return Ok("test endpoint working.");
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout() 
         => await NoContent(async () =>
        {
            await _userService.Logout();
            _logger.LogInformation("User logged out successfully.");
        });


    }
}