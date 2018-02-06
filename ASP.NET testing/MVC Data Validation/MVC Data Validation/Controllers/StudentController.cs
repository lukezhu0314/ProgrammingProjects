using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using MVC_Data_Validation.Models;

namespace MVC_Data_Validation.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student  
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/Create  
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create  
        [HttpPost]
        public ActionResult Create(StudentModel student)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}