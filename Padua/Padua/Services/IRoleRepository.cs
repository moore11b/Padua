using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabW11Authentication.Services
{
    public interface IRoleRepository
    {
        IQueryable<IdentityRole> ReadAll();
    }
}
