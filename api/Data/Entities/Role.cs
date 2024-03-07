using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Role : IdentityRole
    {
        public Role() { }

        public Role(string roleName) : base(roleName) { }


        public bool AuthenticatorRequired { get; set; }

        public bool CheckIp { get; set; }



        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
