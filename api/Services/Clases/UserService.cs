using AutoMapper;
using Data.DTOs.AccountDTO;
using Data.Entities;
using Data.Exceptions;
using Data.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Services.Interfaces.CoreInterfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;


using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Services.Clases;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UrlEncoder _urlEncoder;
    private readonly IMapper _mapper;
    private readonly IDataContext _context;
    private readonly IConfiguration _configuration;
    private User? _user;

    public UserService(UserManager<User> userManager,
        SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor,
        IDataContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    private string DomainUrl => $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}";


    public async Task<IdentityResult> Register(UserRegistrationDTO dto, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new BusinessException($"User with email {dto.Email} already exists.");
        var user = new User
        {
            UserName = dto.UserName,
            FirstName = dto.FirstName?.Trim(),
            LastName = dto.LastName?.Trim(),
            Email = dto.Email,
            Version = "0.0.0.1",
        };
        var result = await _userManager.CreateAsync(user, dto.Password);
        await this._context.SaveChangesAsync(cancellationToken);

        return result;
    }

    public async Task<AuthResultDTO> Login(LoginDTO dto, CancellationToken cancellationToken = default)
    {

        _user = await _userManager.Users
                .Include(o => o.UserPermissions).ThenInclude(o => o.Permission)
                .FirstOrDefaultAsync(u => u.Email == dto.Email || u.UserName == dto.Email, cancellationToken) ?? throw new UserNotExistsException();

        // Check password
        if (!await _userManager.CheckPasswordAsync(_user, dto.Password))
            throw new WrongCredentialsException("Incorrect password.");

        var loggedUser = _mapper.Map<UserDTO>(_user);

        return new AuthResultDTO { UserId = _user.Id, LoggedUser = loggedUser };
    }

    public async System.Threading.Tasks.Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<string> CreateLogoutTokenAsync()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims, "expired");
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public async Task<string> CreateTokenAsync()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims, "expiresIn");
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }


    private SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = _configuration.GetSection("jwtConfig");
        var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }


    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, string loginOption)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");
        var tokenOptions = new JwtSecurityToken
        (
        issuer: jwtSettings["validIssuer"],
        audience: jwtSettings["validAudience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings[loginOption])),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}