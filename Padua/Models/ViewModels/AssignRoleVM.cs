using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabW11Authentication.Models.ViewModels
{
    public class AssignRoleVM
    {
        public ICollection<string> UserNames { get; set; }
        public ICollection<string> RoleNames { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
