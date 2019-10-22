using LabW11Authentication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Padua.Models.Entities
{
    public class Devices
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DevName { get; set; }
        public string DevMAC { get; set; }
    }
}
