using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuICR_WebApp.Models;
using QuICR_WebApp.Models.GraphicsModel;
using QuICR_WebApp.Servives;

namespace QuICR_WebApp.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            var deserializerService = new JsonDeserializerService();
            var polygonGeometry = deserializerService.Deserializer("listofPolygons(100x50).json");

            var databaseService = new DatabaseService();
            var travelTimes = databaseService.ReadData();
            var fillColors = new List<Color>();
            
            foreach(var travelTime in travelTimes)
            {
                var probService = new ProbabilityService();
                var responseFeature = probService.GetProbability(travelTime);
                var fillColor = StyleService.DefaultFilledStyleFromProbability(responseFeature);
                fillColors.Add(fillColor);
            }

            var viewModel = new ViewModel()
            {
                PolygonGeometries = polygonGeometry,
                FillColors = fillColors
            };
           
            return View("Index", viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}