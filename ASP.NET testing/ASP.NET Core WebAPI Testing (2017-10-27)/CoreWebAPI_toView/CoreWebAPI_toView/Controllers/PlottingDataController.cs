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
    [Route("api/PlottingData")]
    public class PlottingDataController : Controller
    {
        // GET: api/PlottingData
        [HttpGet]
        public MatrixData Get()
        {
            randomNumberGenerator RNG = new randomNumberGenerator();
            
            MatrixData PlottingData = new MatrixData
            {
                RgbDataR = RNG.fakeR,
                RgbDataG = RNG.fakeG,
                RgbDataB = RNG.fakeB,
            };

            return PlottingData;
        }

        // GET: api/PlottingData/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/PlottingData
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/PlottingData/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
