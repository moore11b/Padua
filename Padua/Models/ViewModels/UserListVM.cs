using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabW11Authentication.Models.ViewModels
{
    public class UserListVM
    {
            public string Email { get; set; }
            public string UserName { get; set; }
            public int NumberOfRoles { get; set; }
            public string UserId { get; set; }
    }
}
