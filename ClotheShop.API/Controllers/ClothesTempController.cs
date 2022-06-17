using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClotheShop.API.Data;
using ClotheShop.API.Models;

namespace ClotheShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesTempController : ControllerBase
    {
        private readonly ClotheDbContext _context;

        public ClothesTempController(ClotheDbContext context)
        {
            _context = context;
        }

        // GET: api/ClothesTemp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clothe>>> GetClothes()
        {
          if (_context.Clothes == null)
          {
              return NotFound();
          }
            return await _context.Clothes.ToListAsync();
        }

        // GET: api/ClothesTemp/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clothe>> GetClothe(int id)
        {
          if (_context.Clothes == null)
          {
              return NotFound();
          }
            var clothe = await _context.Clothes.FindAsync(id);

            if (clothe == null)
            {
                return NotFound();
            }

            return clothe;
        }

        // PUT: api/ClothesTemp/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClothe(int id, Clothe clothe)
        {
            if (id != clothe.ID)
            {
                return BadRequest();
            }

            _context.Entry(clothe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClotheExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ClothesTemp
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clothe>> PostClothe(Clothe clothe)
        {
          if (_context.Clothes == null)
          {
              return Problem("Entity set 'ClotheDbContext.Clothes'  is null.");
          }
            _context.Clothes.Add(clothe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClothe", new { id = clothe.ID }, clothe);
        }

        // DELETE: api/ClothesTemp/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClothe(int id)
        {
            if (_context.Clothes == null)
            {
                return NotFound();
            }
            var clothe = await _context.Clothes.FindAsync(id);
            if (clothe == null)
            {
                return NotFound();
            }

            _context.Clothes.Remove(clothe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClotheExists(int id)
        {
            return (_context.Clothes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
