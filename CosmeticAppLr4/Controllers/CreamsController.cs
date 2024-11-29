using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CosmeticAppLr4.Models;
using Microsoft.VisualBasic;

namespace CosmeticAppLr4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreamsController : ControllerBase
    {
        private readonly CreamContext _context;
        public CreamsController(CreamContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cream>>> GetCreams(string name)
        {
            return await _context.Creams.Where(cream => name == null || cream.Name == name).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Cream>> GetCream(int id)
        {
            var cream = await _context.Creams.FindAsync(id);
            if (cream == null)
            {
                return NotFound();
            }
            return cream;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCream(int id, Cream cream)
        {
            if (id != cream.Id)
            {
                return BadRequest();
            }
            _context.Entry(cream).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreamExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return CreatedAtAction("GetCream", new { id = cream.Id }, cream);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCream(int id) 
        {
            var cream = await _context.Creams.FindAsync(id);
            if(cream == null)
            {
                return NotFound();
            }
            _context.Creams.Remove(cream);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool CreamExists(int id)
        {
            return _context.Creams.Any(e => e.Id == id);
        }
    }
}