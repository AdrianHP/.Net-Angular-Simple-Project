using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
       
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();

        public string Version { get; set; }

    }
}
