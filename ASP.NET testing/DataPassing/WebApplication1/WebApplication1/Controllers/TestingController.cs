using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TestingController : Controller
    {
        public IActionResult Index()
        {
            LargeMatrix.DataInput();

            /*
            ViewBag.colorRGB = LargeMatrix.colorRGB;
            ViewBag.probabilityMatrix = LargeMatrix.probabilityMatrix;
            */

            ViewBag.colorRGB_one = LargeMatrix.colorRGB_one;
            ViewBag.colorRGB_two = LargeMatrix.colorRGB_two;
            ViewBag.colorRGB_three = LargeMatrix.colorRGB_three;
            ViewBag.probabilityMatrix_one = LargeMatrix.probabilityMatrix_one;
            ViewBag.probabilityMatrix_two = LargeMatrix.probabilityMatrix_two;
            
            return View();
        }
    }
}