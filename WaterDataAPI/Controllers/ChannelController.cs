using Microsoft.AspNetCore.Mvc;
using WaterDataAPI.Models;
using WaterDataAPI.Models.Concrete;

namespace WaterDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : Controller
    {
        private static List<Channel> channels= new List<Channel>
            {
                new Channel{ Id =1, Name ="River1"},
                new Channel{ Id =2, Name ="River2"}
            };

        [HttpGet]
        public async Task<ActionResult<List<Channel>>> Get()
        {
           
            return Ok(channels);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Channel>> Get(int id)
        {
            var river = channels.Find(r => r.Id== id);
            if (river == null) return BadRequest("River not found");

            return Ok(river);
        }
        [HttpPost]
        public async Task<ActionResult<List<Channel>>> AddRiver(Channel river)
        {
            channels.Add(river);
            return Ok(channels);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Channel>>> UpdateRiver([FromBody] Channel request)
        {
            var channel = channels.Find(r => r.Id == request.Id);
            if (channel == null) return BadRequest("River not found");

            //river.Lat = request.Lat;
            //river.Lon = request.Lon;
            //river.StandardWaterLevel = request.StandardWaterLevel;
            //river.WaterFlow = request.WaterFlow; 
            channel.CriticalWaterLevel = request.CriticalWaterLevel;
            channel.Name = request.Name;
            channel.CurrentWaterHeight = request.CurrentWaterHeight;
            channel.PollutionLevel = request.PollutionLevel;
            channel.StandardWaterHeight = request.StandardWaterHeight;
            channel.Id = request.Id;

            return Ok(channels);
        }
    }
}
