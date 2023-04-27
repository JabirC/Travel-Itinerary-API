using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
          var response_body = new Response();
          if (_context.Attractions == null)
          {
                response_body.statusCode = 404;
                response_body.statusDescription = "Database table does not exist";
                return response_body;
          }
            var results =  await _context.Attractions.ToListAsync();

            response_body.statusCode = 200;
            response_body.statusDescription = "Success";
            response_body.data = results;
            return response_body;

        }

        // GET: api/Attractions/country/USA
        [HttpGet("country/{country}")]
        public async Task<ActionResult<Response>> GetAttractions(string country)
        {
          var response_body = new Response();
          if (_context.Attractions == null)
          {
                response_body.statusCode = 404;
                response_body.statusDescription = "Database table does not exist";
                return response_body;
            }
            var attractions = await _context.Attractions.Where(c => c.Country == country).ToListAsync();

            if (attractions == null)
            {
                response_body.statusCode = 404;
                response_body.statusDescription = "ToListAsync() returned null";
                return response_body;
            }

            response_body.statusCode = 200;
            response_body.statusDescription = "Success";
            response_body.data = attractions;
            return response_body;
        }

        // GET: api/Attractions/city/NYC
        [HttpGet("city/{city}")]
        public async Task<ActionResult<Response>> GetAttractionsCity(string city)
        {
            var response_body = new Response();
            if (_context.Attractions == null)
            {
                response_body.statusCode = 404;
                response_body.statusDescription = "Failure: Database table does not exist";
                return response_body;
            }
            var attractions = await _context.Attractions.Where(c => c.City == city).ToListAsync();

            if (attractions == null)
            {
                response_body.statusCode = 404;
                response_body.statusDescription = "Failure: ToListAsync() returned null";
                return response_body;
            }

            response_body.statusCode = 200;
            response_body.statusDescription = "Success";
            response_body.data = attractions;
            return response_body;
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

        // POST: api/Attractions/AddAttraction"
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostAttractions(Attractions attractions)
        {
          var response_body = new Response();
          if (_context.Attractions == null)
          {
                response_body.statusCode = 404;
                response_body.statusDescription = "Failure: Database table does not exist";
                return response_body;
            }

            var results = await _context.Attractions.Where(c => c.Address == attractions.Address).FirstOrDefaultAsync();
            if(results != null)
            {
                response_body.statusCode = 400;
                response_body.statusDescription = "Failure: Address already exists in the database";
                return response_body;
            }
            _context.Attractions.Add(attractions);
            await _context.SaveChangesAsync();

            response_body.statusCode = 200;
            response_body.statusDescription = "Success: Entry added to the Attractions table";
            return response_body;
        }


        private bool AttractionsExists(int id)
        {
            return (_context.Attractions?.Any(e => e.AttractionsId == id)).GetValueOrDefault();
        }
    }
}
