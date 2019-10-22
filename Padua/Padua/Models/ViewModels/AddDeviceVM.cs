using Padua.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LabW11Authentication.Models.ViewModels;
using LabW11Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabW11Authentication.Models.ViewModels
{
    public class AddDeviceVM
    {
        [Required(ErrorMessage = "A Device Name is Required")]
        [DisplayName("Device Name")]
        public string DevName { get; set; }
        [Required(ErrorMessage = "A Device MAC Address is Required")]
        [RegularExpression("^[a-fA-F0-9:]{17}|[a-fA-F0-9]{12}$", ErrorMessage = "MAC addresses be in format - XX:XX:XX:XX:XX:XX")]
        [DisplayName("Device MAC Address")]
        public string DevMAC { get; set; }
        [NotMapped]
        public string UserId { get; set; }

        public Devices GetDevicesInstance()
        {
            return new Devices
            {
                Id = 0,
                UserId = UserId,
                DevName = DevName,
                DevMAC = DevMAC
            };
        }
    }
}