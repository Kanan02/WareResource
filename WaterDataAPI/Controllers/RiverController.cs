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
                new River{ Id =1, Name ="River1", AirTemperature=20, CurrentWaterLevel=15, StandardWaterLevel=10, PollutionPercentage=35, SpeedOfWater=5.6},
                new River{ Id =2, Name ="River1", AirTemperature=25, CurrentWaterLevel=17, StandardWaterLevel=15, PollutionPercentage=40, SpeedOfWater=5.6},
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
            if (river == null) return NotFound();

            return Ok(river);
        }
        [HttpPost]
        public async Task<ActionResult<List<River>>> AddRiver(River river)
        {
            rivers.Add(river);
            return Ok(rivers);
        }

    }
}
