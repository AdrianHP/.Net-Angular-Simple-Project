using Data.DTOs.AccountDTO;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Services.Interfaces
{
    public interface IUserService
    {
       
        Task<IdentityResult> Register(UserRegistrationDTO dto, CancellationToken cancellationToken = default);
       
        Task<AuthResultDTO> Login(LoginDTO dto, CancellationToken cancellationToken = default);
      
        Task Logout();

        Task<string> CreateTokenAsync();

        Task<string> CreateLogoutTokenAsync();

    }
}
