using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterDataAPI.Data;
using WaterDataAPI.Models.Concrete;

namespace WaterDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FieldController : Controller
    {
        private readonly DataContext _context;
        public FieldController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Field>>> Get()
        {

            return Ok(await _context.WaterReservoirs.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Field>> Get(int id)
        {
            var res = await _context.Fields.FindAsync(id);
            if (res == null) return BadRequest("Field not found");

            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<List<Field>>> AddField(Field f)
        {
            _context.Fields.Add(f);
            await _context.SaveChangesAsync();
            return Ok(_context.Fields.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<WaterReservoir>>> Update([FromBody] Field request)
        {
            var res = await _context.Fields.FindAsync(request.Id);
            if (res == null) return BadRequest("Field not found");
            res.Name = request.Name;
            res.SAT = request.SAT;
            res.Wl = request.Wl;
            res.PERC = request.PERC;
            res.Stage = request.Stage;
            await _context.SaveChangesAsync();
            return Ok(_context.Fields.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RainWaterReservoir>>> Delete(int id)
        {
            var reservoir = await _context.Fields.FindAsync(id);
            if (reservoir == null) return BadRequest("Field not found");
            _context.Fields.Remove(reservoir);
            await _context.SaveChangesAsync();
            return Ok(_context.Fields.ToListAsync());
        }
    } 
}
