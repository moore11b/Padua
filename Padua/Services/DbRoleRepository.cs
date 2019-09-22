using LabW11Authentication.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabW11Authentication.Services
{
    public class DbRoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _db;
        public DbRoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<IdentityRole> ReadAll()
        {
            var list = _db.Roles.ToList();
            var query = list.AsQueryable();
            return query;
        }

    }
}
