using LabW11Authentication.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabW11Authentication.Services
{
    /// <summary>
    /// This class serves to initialze the database on first run with given roles below as possible user options and a default admin account is created.
    /// </summary>
    public class Initializer
    {
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public Initializer(
           ApplicationDbContext db,
           UserManager<IdentityUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedUsersAsync()
        {
            _db.Database.EnsureCreated();

            if (!_db.Roles.Any(r => r.Name == "Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }

            if (!_db.Roles.Any(r => r.Name == "Student"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Student" });
            }

            if (!_db.Users.Any(u => u.UserName == "admin@test.com"))
            {
                var user = new IdentityUser
                {
                    Email = "admin@test.com",
                    UserName = "admin@test.com"
                };
                await _userManager.CreateAsync(user, "Pass1!");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }

    }
}
