using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            Seat s = new Seat() { SeatNumber = "20", Price = 300};
            return View(s);
        }
    }
}