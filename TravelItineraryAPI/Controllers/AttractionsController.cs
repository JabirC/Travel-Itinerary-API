using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelItineraryAPI.Models;

namespace TravelItineraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionsController : ControllerBase
    {
        private readonly TravelItineraryAPIDBContext _context;

        public AttractionsController(TravelItineraryAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Attractions
        [HttpGet]
        public async Task<ActionResult<Response>> GetAttractions()
        {
          if (_context.Attractions == null)
          {
              return NotFound();
          }
            var results =  await _context.Attractions.ToListAsync();
            var response_body = new Response();
            response_body.statusCode = 200;
            response_body.statusDescription = "Success";
            response_body.data = results; 
            return response_body;

        }

        // GET: api/Attractions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attractions>> GetAttractions(int id)
        {
          if (_context.Attractions == null)
          {
              return NotFound();
          }
            var attractions = await _context.Attractions.FindAsync(id);

            if (attractions == null)
            {
                return NotFound();
            }

            return attractions;
        }

        // PUT: api/Attractions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttractions(int id, Attractions attractions)
        {
            if (id != attractions.AttractionsId)
            {
                return BadRequest();
            }

            _context.Entry(attractions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttractionsExists(id))
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

        // POST: api/Attractions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attractions>> PostAttractions(Attractions attractions)
        {
          if (_context.Attractions == null)
          {
              return Problem("Entity set 'TravelItineraryAPIDBContext.Attractions'  is null.");
          }
            _context.Attractions.Add(attractions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttractions", new { id = attractions.AttractionsId }, attractions);
        }

        // DELETE: api/Attractions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttractions(int id)
        {
            if (_context.Attractions == null)
            {
                return NotFound();
            }
            var attractions = await _context.Attractions.FindAsync(id);
            if (attractions == null)
            {
                return NotFound();
            }

            _context.Attractions.Remove(attractions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttractionsExists(int id)
        {
            return (_context.Attractions?.Any(e => e.AttractionsId == id)).GetValueOrDefault();
        }
    }
}
