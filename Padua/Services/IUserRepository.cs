using LabW11Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Padua.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabW11Authentication.Services
{
    public interface IUserRepository
    {
        IQueryable<ApplicationUser> ReadAll();
        IQueryable<Devices> ReadAllDevices();
        Devices AddDev(Devices device);
        ApplicationUser Read(string userName);
        bool AssignRole(string userName, string roleName);
        Task<ApplicationUser> CreateAsync(IdentityUser identityUser, string password);
        bool Exists(string username);
        Devices ReadDeviceId(int devId);
        void UpdateDevice(int id, Devices device);
        void DeleteDevice(int id);
    }
}
