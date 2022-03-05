using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterDataAPI.Data;
using WaterDataAPI.Models.Concrete;

namespace WaterDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroundWaterReservoirController : Controller
    {
        private readonly DataContext _context;
        public GroundWaterReservoirController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<GroundWaterReservoir>>> Get()
        {

            return Ok(await _context.GroundWaterReservoirs.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GroundWaterReservoir>> Get(int id)
        {
            var res = await _context.GroundWaterReservoirs.FindAsync(id);
            if (res == null) return BadRequest("Reservoir not found");

            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<List<GroundWaterReservoir>>> AddReservoir(GroundWaterReservoir reservoir)
        {
            _context.GroundWaterReservoirs.Add(reservoir);
            await _context.SaveChangesAsync();
            return Ok(_context.GroundWaterReservoirs.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<GroundWaterReservoir>>> UpdateRiver([FromBody] GroundWaterReservoir request)
        {
            var res = await _context.GroundWaterReservoirs.FindAsync(request.Id);
            if (res == null) return BadRequest("Channel not found");
            res.CurrentWaterLevel = request.CurrentWaterLevel;
            res.Name = request.Name;
            res.Length = request.Length;
            res.PollutionLevel = request.PollutionLevel;
            res.Width = request.Width;
            
            await _context.SaveChangesAsync();
            return Ok(_context.GroundWaterReservoirs.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GroundWaterReservoir>>> Delete(int id)
        {
            var reservoir = await _context.GroundWaterReservoirs.FindAsync(id);
            if (reservoir == null) return BadRequest("GroundWaterReservoir not found");
            _context.GroundWaterReservoirs.Remove(reservoir);
            await _context.SaveChangesAsync();
            return Ok(_context.GroundWaterReservoirs.ToListAsync());

        }

    }
}
