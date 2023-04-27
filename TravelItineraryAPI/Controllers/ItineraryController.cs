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
    public class ItineraryController : ControllerBase
    {
        private readonly TravelItineraryAPIDBContext _context;

        public ItineraryController(TravelItineraryAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Itinerary
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Itinerary>>> GetItinerary()
        {
          if (_context.Itinerary == null)
          {
              return NotFound();
          }
            return await _context.Itinerary.ToListAsync();
        }

        // GET: api/Itinerary/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Itinerary>> GetItinerary(int id)
        {
          if (_context.Itinerary == null)
          {
              return NotFound();
          }
            var itinerary = await _context.Itinerary.FindAsync(id);

            if (itinerary == null)
            {
                return NotFound();
            }

            return itinerary;
        }

        // PUT: api/Itinerary/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItinerary(int id, Itinerary itinerary)
        {
            if (id != itinerary.ItineraryId)
            {
                return BadRequest();
            }

            _context.Entry(itinerary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItineraryExists(id))
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

        // POST: api/Itinerary
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Itinerary>> PostItinerary(Itinerary itinerary)
        {
          if (_context.Itinerary == null)
          {
              return Problem("Entity set 'TravelItineraryAPIDBContext.Itinerary'  is null.");
          }
            _context.Itinerary.Add(itinerary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItinerary", new { id = itinerary.ItineraryId }, itinerary);
        }

        // DELETE: api/Itinerary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItinerary(int id)
        {
            if (_context.Itinerary == null)
            {
                return NotFound();
            }
            var itinerary = await _context.Itinerary.FindAsync(id);
            if (itinerary == null)
            {
                return NotFound();
            }

            _context.Itinerary.Remove(itinerary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItineraryExists(int id)
        {
            return (_context.Itinerary?.Any(e => e.ItineraryId == id)).GetValueOrDefault();
        }
    }
}
