using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabW11Authentication.Models.ViewModels;
using LabW11Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LabW11Authentication.Controllers
{

    /// <summary>
    /// This class exists solely for admins only. This is for admins to create users if they cannot themselves, read all users, and assign roles to users for additional admins or security staff/personnel accounts
    /// </summary>
    /// 
    [Authorize]
    public class UserController : Controller
    {
        private IUserRepository _repo;
        private IRoleRepository _repo2;
        public UserController(IUserRepository repo, IRoleRepository repo2)
        {
            _repo = repo;
            _repo2 = repo2;
        }

        /// <summary>
        /// If account is admin, show all users and their roles.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var user = _repo.Read(User.Identity.Name);
            if(!user.HasRole("Admin"))
            {
                return LocalRedirect("/Identity/Account/AccessDenied");
            }

            var users = _repo.ReadAll();
            var userList = users
               .Select(u => new UserListVM
               {
                   Email = u.User.Email,
                   UserName = u.User.UserName,
                   NumberOfRoles = u.Roles.Count
               });
            return View(userList);

        }

        /// <summary>
        /// If account is admin, change or assign new roles to users.
        /// </summary>
        /// <returns></returns>
        public IActionResult AssignRoles()
        {
            var user = _repo.Read(User.Identity.Name);
            if (!user.HasRole("Admin"))
            {
                return LocalRedirect("/Identity/Account/AccessDenied");
            }

            var users = _repo.ReadAll().Select(u=>u.User.UserName).ToList();
            var roles = _repo2.ReadAll().Select(r=>r.Name).ToList();
            var model = new AssignRoleVM
            {
                UserNames = users,
                RoleNames = roles
            };
            return View(model);
        }

        /// <summary>
        /// Submit role changes to DB
        /// </summary>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AssignRoles(AssignRoleVM model)
        {
            _repo.AssignRole(model.UserName, model.RoleName);
            return RedirectToAction("Index", "User");
        }

        public IActionResult Create()
        {
            var user = _repo.Read(User.Identity.Name);
            if(!user.HasRole("Admin"))
            {
                return LocalRedirect("/Identity/Account/AccessDenied");
            }
            return View();
        }

        /// <summary>
        /// New user creation and verification of no conflicting user names.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVM userVM)
        {
            var identityUser = new IdentityUser
            {
                Email = userVM.UserName,
                UserName = userVM.UserName
            };

            if (!_repo.Exists(identityUser.UserName))
            {
                ModelState.AddModelError("Username", "Username already exists!");
                return View(userVM);
            }

            await _repo.CreateAsync(identityUser, userVM.Password);

            return RedirectToAction("Index", "User");
        }
    }
}