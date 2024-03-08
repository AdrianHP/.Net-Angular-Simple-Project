using AutoMapper;
using Data.DTOs.AccountDTO;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mappings
{
    public class AccountProfile: Profile
    {
        public AccountProfile()
        {
            CreateMap<User, UserDTO>()
                    .ForMember(d => d.Permissions, m => m.MapFrom(s => s.UserPermissions))
                    .ForMember(d => d.Password, opts => opts.Ignore())
                    .ForMember(d => d.ConfirmPassword, opts => opts.Ignore())
                .ReverseMap()
                    .ForMember(d => d.NormalizedEmail, m => m.MapFrom(s => s.Email.ToUpperInvariant()))
                    .ForMember(d => d.NormalizedUserName, m => m.MapFrom(s => s.UserName.ToUpperInvariant()))
                    .ForMember(d => d.UserPermissions, m => m.Ignore());
        }
    }
}
