using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MongoDB_GoogleAPIData_Coloring.Model;

namespace MongoDB_GoogleAPIData_Coloring.Controllers
{
    public class ArcGISController : Controller
    {
        public IActionResult Index()
        {
            var ringCollection_100x50 = GetJson.GetJsonFunc("ListofPolygons(100x50).json");
            //var ringCollection_200x100 = GetJson.GetJsonFunc("ListofPolygons(200x100).json");
            //var ringCollection_300x150 = GetJson.GetJsonFunc("ListofPolygons(300x150).json");
            var RGBList_100x50 = MongoDBClass.RGBGeneration();
            //var RGBList_100x50 = MongoDBClass.RandomRGBGeneration(ringCollection_100x50.Count);
            //var RGBList_200x100 = MongoDBClass.RandomRGBGeneration(ringCollection_200x100.Count);
            //var RGBList_300x150 = MongoDBClass.RandomRGBGeneration(ringCollection_300x150.Count);
            
            var viewModel_100x50 = new ViewModel
            {
                RGBList = RGBList_100x50,
                ringCollection = ringCollection_100x50
            };

            /*
            var viewModel_200x100 = new ViewModel
            {
                RGBList = RGBList_200x100,
                ringCollection = ringCollection_200x100
            };

            var viewModel_300x150 = new ViewModel
            {
                RGBList = RGBList_300x150,
                ringCollection = ringCollection_300x150
            };
            */
            var listofVM = new ListofViewModels{
                VM = new List<ViewModel>()
            };
            listofVM.VM.Add(viewModel_100x50);
            //listofVM.VM.Add(viewModel_200x100);
            //listofVM.VM.Add(viewModel_300x150);

            return View("Index",listofVM);
        }

        public class ViewModel {
            public ListofRGB RGBList { get; set; }
            public List<RingCollection> ringCollection { get; set; }
        }

        public class ListofViewModels {
            public List<ViewModel> VM {get; set;}
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
            return View();
        }
    }
}