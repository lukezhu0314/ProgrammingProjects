using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartialViewProject.Models;

namespace PartialViewProject.Controllers
{
    public class MyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(TimeConstant TC)
        {
            ViewBag.TC = TC;
            return PartialView("_MyView");
        }
    }
}