using Microsoft.AspNetCore.Mvc;
using WaterDataAPI.Models;

namespace WaterDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiverController : Controller
    {
        private static List<River> rivers= new List<River>
            {
                new River{ Id =1, Name ="River1", AirTemperature=20, CurrentWaterLevel=15, StandardWaterLevel=10, PollutionPercentage=35, WaterFlow=5.6},
                new River{ Id =2, Name ="River2", AirTemperature=25, CurrentWaterLevel=17, StandardWaterLevel=15, PollutionPercentage=40, WaterFlow=5.6},
            };

        [HttpGet]
        public async Task<ActionResult<List<River>>> Get()
        {
           
            return Ok(rivers);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<River>> Get(int id)
        {
            var river = rivers.Find(r => r.Id== id);
            if (river == null) return BadRequest("River not found");

            return Ok(river);
        }
        [HttpPost]
        public async Task<ActionResult<List<River>>> AddRiver(River river)
        {
            rivers.Add(river);
            return Ok(rivers);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<River>>> UpdateRiver([FromBody]River request)
        {
            var river = rivers.Find(r => r.Id == request.Id);
            if (river == null) return BadRequest("River not found");

            river.Lat = request.Lat;
            river.Lon = request.Lon;
            river.StandardWaterLevel = request.StandardWaterLevel;
            river.WaterFlow = request.WaterFlow; 
            river.CurrentWaterLevel = request.CurrentWaterLevel;
            river.Name = request.Name;

            return Ok(rivers);
        }
    }
}
