using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.AccountDTO
{
    public class AuthResultDTO
    {
        public string UserId { get; set; }
        public UserDTO LoggedUser { get; set; }
    }
}
