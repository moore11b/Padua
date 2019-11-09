using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LabW11Authentication.Models.ViewModels;
using LabW11Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LabW11Authentication.Controllers
{

    /// <summary>
    /// This class exists to handle all user interaction.
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
        [Authorize(Roles = "Admin")]
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
                   NumberOfRoles = u.Roles.Count,
                   UserId = u.User.Id
               });
            return View(userList);
        }

        /// <summary>
        /// If account is admin, show all device dbo data.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public IActionResult DeviceList()
        {
            var user = _repo.Read(User.Identity.Name);
            if (!user.HasRole("Admin"))
            {
                return LocalRedirect("/Identity/Account/AccessDenied");
            }

            var devices = _repo.ReadAllDevices();
            var deviceList = devices
               .Select(u => new DeviceListVM
               {
                   Id = u.Id,
                   DevName = u.DevName,
                   DevMAC = u.DevMAC,
                   UserId = u.UserId
               });
            return View(deviceList);
        }

        /// <summary>
        /// Pull all devices for current logged in user and pass to view for formatting.
        /// Must be logged in to view, validated in Identity
        /// </summary>
        /// <returns></returns>
        public IActionResult Devices()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var devices = _repo.ReadAllDevices();
            var deviceList = devices
               .Select(u => new DeviceListVM
               {
                   Id = u.Id,
                   DevName = u.DevName,
                   DevMAC = u.DevMAC,
                   UserId = u.UserId
               });

            ViewBag.Message = userId;
            return View(deviceList);
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
        /// Submit role changes to DB (as admin editing accounts)
        /// </summary>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AssignRoles(AssignRoleVM model)
        {
            _repo.AssignRole(model.UserName, model.RoleName);
            return RedirectToAction("Index", "User");
        }

        /// <summary>
        /// only admin has access to manually create users
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// add device to user account
        /// </summary>
        /// <returns></returns>
        public IActionResult AddDev()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return View("Devices", "User");
            }
            ViewBag.Message = userId;
            return View();
        }

        /// <summary>
        /// New device addition and verification of no conflicting MAC addrs.
        /// </summary>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddDev(AddDeviceVM deviceVM)
        {
            if (ModelState.IsValid)
            {
                var device = deviceVM.GetDevicesInstance();
                _repo.AddDev(device);
                return RedirectToAction("Devices", "User");
            }
            return View(deviceVM);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="recId"></param>
        /// <returns></returns>
        public IActionResult DeleteDevice([Bind(Prefix = "id")]int devId)
        {
            var device = _repo.ReadDeviceId(devId);
            if (device == null)
            {
                return RedirectToAction("Devices", "User");
            }
            return View(device);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteDevice(id);
            return RedirectToAction("Devices", "User");
        }
    }
}