using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreWebAPI_toView.Models;

namespace CoreWebAPI_toView.Controllers
{
    [Produces("application/json")]
    [Route("api/StudentAPI")]
    public class StudentAPIController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "value1";
        }
        
        // POST: api/studentAPI
        
    }
}
