using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.AccountDTO
{
    public class RoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool CheckIp { get; set; }

        public ICollection<PermissionDTO> Permissions { get; set; }
    }
}
