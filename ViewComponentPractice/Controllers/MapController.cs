using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewComponentPractice.Models;

namespace ViewComponentPractice.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            var screeningTools = new List<SelectListItem>()
            {
                new SelectListItem {Text = "LAMS", Value = "LAMS", Selected = true},
                new SelectListItem {Text = "RACE", Value = "RACE"},
                new SelectListItem {Text = "C-STAT", Value = "C-STAT"}
            };
            Parameters parameters = new Parameters()
            {
                ScreeningTools = screeningTools 
            };

            return View(parameters);
        }
        [HttpPost]
        public IActionResult About(Parameters parameters)
        {
            ViewData["Message"] = "Your application description page.";

            return View(parameters);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
    }
}