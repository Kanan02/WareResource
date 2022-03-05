using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterDataAPI.Data;
using WaterDataAPI.Models.Concrete;

namespace WaterDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainWaterReservoirController : Controller
    {
       
        private readonly DataContext _context;
        public RainWaterReservoirController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<RainWaterReservoir>>> Get()
        {

            return Ok(await _context.RainWaterReservoirs.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RainWaterReservoir>> Get(int id)
        {
            var res = await _context.RainWaterReservoirs.FindAsync(id);
            if (res == null) return BadRequest("Reservoir not found");

            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<List<RainWaterReservoir>>> AddReservoir(RainWaterReservoir reservoir)
        {
            _context.RainWaterReservoirs.Add(reservoir);
            await _context.SaveChangesAsync();
            return Ok(_context.RainWaterReservoirs.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<RainWaterReservoir>>> UpdateRiver([FromBody] RainWaterReservoir request)
        {
            var res = await _context.RainWaterReservoirs.FindAsync(request.Id);
            if (res == null) return BadRequest("RainWaterReservoirs not found");
            res.CurrentWaterLevel = request.CurrentWaterLevel;
            res.Name = request.Name;
            res.Length = request.Length;
            res.PollutionLevel = request.PollutionLevel;
            res.Width = request.Width;

            await _context.SaveChangesAsync();
            return Ok(_context.RainWaterReservoirs.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RainWaterReservoir>>> Delete(int id)
        {
            var reservoir = await _context.RainWaterReservoirs.FindAsync(id);
            if (reservoir == null) return BadRequest("RainWaterReservoirs not found");
            _context.RainWaterReservoirs.Remove(reservoir);
            await _context.SaveChangesAsync();
            return Ok(_context.RainWaterReservoirs.ToListAsync());
        }
    }
}
