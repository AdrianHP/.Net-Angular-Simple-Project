using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.AccountDTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
       
        public ICollection<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
        public ICollection<PermissionDTO> Permissions { get; set; } = new List<PermissionDTO>();

    }
}
