using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LabW11Authentication.Controllers
{

    /// <summary>
    /// This class will manage student devices and other related information.
    /// </summary>
    public class StudentController : Controller
    {
        /// <summary>
        /// Undetermined.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Pull all devices for current logged in user and pass to view for formatting.
        /// </summary>
        /// <returns></returns>
        public IActionResult Devices()
        {
            return View();
        }
    }
}