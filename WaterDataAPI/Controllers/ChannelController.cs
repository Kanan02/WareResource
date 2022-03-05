using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterDataAPI.Data;
using WaterDataAPI.Models;
using WaterDataAPI.Models.Concrete;

namespace WaterDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : Controller
    {
        private readonly DataContext _context;
        public ChannelController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Channel>>> Get()
        {
           
            return Ok(await _context.Channels.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Channel>> Get(int id)
        {
            var chan =await _context.Channels.FindAsync(id);
            if (chan == null) return BadRequest("Channel not found");

            return Ok(chan);
        }
        [HttpPost]
        public async Task<ActionResult<List<Channel>>> AddRiver(Channel river)
        {
            _context.Channels.Add(river);
            await _context.SaveChangesAsync();
            return Ok(_context.Channels.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Channel>>> UpdateRiver([FromBody] Channel request)
        {
            var chan = await _context.Channels.FindAsync(request.Id);
            if (chan == null) return BadRequest("Channel not found");
            chan.CriticalWaterLevel = request.CriticalWaterLevel;
            chan.Name = request.Name;
            chan.CurrentWaterHeight = request.CurrentWaterHeight;
            chan.PollutionLevel = request.PollutionLevel;
            chan.StandardWaterHeight = request.StandardWaterHeight;
            chan.Id = request.Id;
            await _context.SaveChangesAsync();
            return Ok(_context.Channels.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Channel>>> Delete(int id)
        {
            var channel = await _context.Channels.FindAsync(id);
            if (channel == null) return BadRequest("Channel not found");
            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();
            return Ok(_context.Channels.ToListAsync());

        }

    }
}
