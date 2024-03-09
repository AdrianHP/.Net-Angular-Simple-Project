using AutoMapper;
using Data.DTOs.AccountDTO;
using Data.DTOs.EntityDTO;
using Data.Entities;
using Data.Exceptions;
using Data.Interfaces;
using Data.SqlServer;
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

public class TaskService : ITaskService
{

    private DataContext _context;
    public readonly DbSet<Data.Entities.Task> Tasks;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public TaskService(DataContext context, UserManager<User> userManager, IMapper mapper)
    {
        _context = context;
        Tasks = context.Tasks;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async System.Threading.Tasks.Task<List<TaskDTO>>GetUserTask(string userName,CancellationToken token)
    {
        var user = await this._userManager.FindByNameAsync(userName);
        var result = await _context.Set<Data.Entities.Task>()
            .Where(x=>x.AssignedUserId == user.Id)
            .ToListAsync();
        return _mapper.Map<List<TaskDTO>>(user.Tasks);
    }



}