using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterDataAPI.Data;
using WaterDataAPI.Models.Concrete;

namespace WaterDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterReservoirController : Controller
    {
        private readonly DataContext _context;
        public WaterReservoirController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<WaterReservoir>>> Get()
        {

            return Ok(await _context.WaterReservoirs.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RainWaterReservoir>> Get(int id)
        {
            var res = await _context.WaterReservoirs.FindAsync(id);
            if (res == null) return BadRequest("Reservoir not found");

            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<List<WaterReservoir>>> AddReservoir(WaterReservoir reservoir)
        {
            _context.WaterReservoirs.Add(reservoir);
            await _context.SaveChangesAsync();
            return Ok(_context.WaterReservoirs.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<WaterReservoir>>> Update([FromBody] WaterReservoir request)
        {
            var res = await _context.WaterReservoirs.FindAsync(request.Id);
            if (res == null) return BadRequest("WaterReservoir not found");
            res.Name = request.Name;
            res.PollutionLevel = request.PollutionLevel;

            await _context.SaveChangesAsync();
            return Ok(_context.WaterReservoirs.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RainWaterReservoir>>> Delete(int id)
        {
            var reservoir = await _context.WaterReservoirs.FindAsync(id);
            if (reservoir == null) return BadRequest("RainWaterReservoirs not found");
            _context.WaterReservoirs.Remove(reservoir);
            await _context.SaveChangesAsync();
            return Ok(_context.WaterReservoirs.ToListAsync());
        }
    }
}
