using Microsoft.EntityFrameworkCore;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.CoreInterfaces
{
    public interface IDataContext : IApiDbContext
    {
        #region Account

        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }
        DbSet<UserPermission> UserPermissions { get; set; }

        #endregion

        #region Entities
        DbSet<Data.Entities.Task> Tasks { get; set; }

        #endregion

    }
}
