using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabW11Authentication.Models.ViewModels
{
    public class DeviceListVM
    {
        public int Id { get; set; }
        public string DevName { get; set; }
        public string DevMAC { get; set; }
        public string UserId { get; set; }
    }
}