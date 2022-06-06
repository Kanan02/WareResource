using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterDataAPI.Data;
using WaterDataAPI.Models.Concrete;

namespace WaterDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FarmController : Controller
    {
        private readonly DataContext _context;
        public FarmController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Farm>>> Get()
        {

            return Ok(await _context.Farms.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Farm>> Get(int id)
        {
            var res = await _context.Farms.FindAsync(id);
            if (res == null) return BadRequest("Farm not found");

            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<List<WaterReservoir>>> AddFarm(Farm f)
        {
            _context.Farms.Add(f);
            await _context.SaveChangesAsync();
            return Ok(_context.Farms.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<WaterReservoir>>> Update([FromBody] Farm request)
        {
            var res = await _context.Farms.FindAsync(request.Id);
            if (res == null) return BadRequest("Farm not found");
            res.Name = request.Name;
            res.City = request.City;

            await _context.SaveChangesAsync();
            return Ok(_context.Farms.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Farm>>> Delete(int id)
        {
            var reservoir = await _context.Farms.FindAsync(id);
            if (reservoir == null) return BadRequest("Farm not found");
            _context.Farms.Remove(reservoir);
            await _context.SaveChangesAsync();
            return Ok(_context.Farms.ToListAsync());
        }
    }
}
