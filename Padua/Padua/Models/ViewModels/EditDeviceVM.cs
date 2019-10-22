using Padua.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabW11Authentication.Models.ViewModels
{
    public class EditDeviceVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A Device Name is Required")]
        [DisplayName("Device Name")]
        public string DevName { get; set; }
        [Required(ErrorMessage = "A Device MAC Address is Required")]
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