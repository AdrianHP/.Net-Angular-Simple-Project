using Data.Interfaces.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Permission : IEntity<int>
    {
        public Permission() { }

        public Permission(string permissionName) => Name = permissionName;


        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    }
}
