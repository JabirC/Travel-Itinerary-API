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


        // GET: api/Itinerary/SerraCanca
        [HttpGet("{UserName}")]
        public async Task<ActionResult<Response>> GetItinerary(string UserName)
        {
          var response_body = new Response();
          if (_context.Itinerary == null)
          {
                response_body.statusCode = 404;
                response_body.statusDescription = "Failure: Database table does not exist";
                return response_body;
            }
            var itinerary = await _context.Itinerary.Where(c => c.UserName == UserName).FirstOrDefaultAsync();
            if (itinerary == null)
            {
                response_body.statusCode = 404;
                response_body.statusDescription = "Failure: User does not exist";
                return response_body;
            }

            var attractions = await _context.Attractions.Where(c => c.AttractionsId == itinerary.Location_1 || c.AttractionsId == itinerary.Location_2 || c.AttractionsId == itinerary.Location_3 || c.AttractionsId == itinerary.Location_4).ToListAsync();


            response_body.statusCode = 200;
            response_body.statusDescription = "Success";
            response_body.data = attractions;
            
            return response_body;
        }



        // Patch: api/Itinerary/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("addItinerary/{UserName}")]
        public async Task<ActionResult<Response>> PatchItinerary(string UserName, Attractions attractions)
        {
            var response_body = new Response();

            var itinerary = await _context.Itinerary.Where(c => c.UserName == UserName).FirstOrDefaultAsync();
            if (itinerary == null)
            {
                response_body.statusCode = 404;
                response_body.statusDescription = "Failure: User does not exist";
                return response_body;
            }

            var temp = await _context.Attractions.Where(c => c.Address == attractions.Address).FirstOrDefaultAsync();

            if(temp == null)
            {
                response_body.statusCode = 404;
                response_body.statusDescription = "Failure: attraction was not found in the database";
                return response_body;
            }

            if(itinerary.Location_1 != null)
            {
                if(itinerary.Location_1 == temp.AttractionsId)
                {
                    response_body.statusCode = 406;
                    response_body.statusDescription = "Failure: Attraction is already in the itinerary";
                    return response_body;
                }

                if(itinerary.Location_2 != null)
                {
                    if (itinerary.Location_2 == temp.AttractionsId)
                    {
                        response_body.statusCode = 406;
                        response_body.statusDescription = "Failure: Attraction is already in the itinerary";
                        return response_body;
                    }
                    if (itinerary.Location_3 != null)
                    {
                        if (itinerary.Location_3 == temp.AttractionsId)
                        {
                            response_body.statusCode = 406;
                            response_body.statusDescription = "Failure: Attraction is already in the itinerary";
                            return response_body;
                        }
                        if (itinerary.Location_4 != null)
                        {
                            if (itinerary.Location_4 == temp.AttractionsId)
                            {
                                response_body.statusCode = 406;
                                response_body.statusDescription = "Failure: Attraction is already in the itinerary";
                                return response_body;
                            }

                            response_body.statusCode = 406;
                            response_body.statusDescription = "Failure: User itinerary is full";
                            return response_body;
                        }

                        itinerary.Location_4 = temp.AttractionsId;
                    }
                    else
                    {
                        itinerary.Location_3 = temp.AttractionsId;
                    }
                }

                else
                {
                    itinerary.Location_2 = temp.AttractionsId;
                }
            }
            else
            {
                itinerary.Location_1 = temp.AttractionsId;
            }


            _context.Entry(itinerary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItineraryExists(UserName))
                {
                    response_body.statusCode = 404;
                    response_body.statusDescription = "Failure: User does not exist";
                    return response_body;
                }
                else
                {
                    response_body.statusCode = itinerary.ItineraryId;
                    response_body.statusDescription = "unexpected error";
                    return response_body;
                }
            }

            response_body.statusCode = 200;
            response_body.statusDescription = "Success: attraction added to Itinerary";
            return response_body;
        }




        // Delete: api/Itinerary/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpDelete("ClearItinerary/{UserName}")]
        public async Task<ActionResult<Response>> DeleteItinerary(string UserName)
        {
            var response_body = new Response();

            var itinerary = await _context.Itinerary.Where(c => c.UserName == UserName).FirstOrDefaultAsync();
            if (itinerary == null)
            {
                response_body.statusCode = 404;
                response_body.statusDescription = "Failure: User does not exist";
                return response_body;
            }

            itinerary.Location_1 = null;
            itinerary.Location_2 = null;
            itinerary.Location_3 = null;
            itinerary.Location_4 = null;

            _context.Entry(itinerary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItineraryExists(UserName))
                {
                    response_body.statusCode = 404;
                    response_body.statusDescription = "Failure: User does not exist";
                    return response_body;
                }
                else
                {
                    response_body.statusCode = itinerary.ItineraryId;
                    response_body.statusDescription = "unexpected error";
                    return response_body;
                }
            }

            response_body.statusCode = 200;
            response_body.statusDescription = "Success: Itinerary Cleared";
            return response_body;
        }

        // POST: api/Itinerary
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> PostItinerary(Itinerary itinerary)
        {
            var response_body = new Response();
            if (_context.Itinerary == null)
            {
                response_body.statusCode = 404;
                response_body.statusDescription = "Failure: Database table does not exist";
                return response_body;
            }

            var users = await _context.Itinerary.Where(c => c.UserName== itinerary.UserName).FirstOrDefaultAsync();
            if(users != null)
            {
                response_body.statusCode = 400;
                response_body.statusDescription = "Failure: User already exists in the database";
                return response_body;
            }

            itinerary.Location_1 = null;
            itinerary.Location_2 = null;
            itinerary.Location_3 = null;
            itinerary.Location_4 = null;
            _context.Itinerary.Add(itinerary);
            await _context.SaveChangesAsync();

            response_body.statusCode = 200;
            response_body.statusDescription = "Success: Entry added to the Itinerary table";
            return response_body;
        }


        private bool ItineraryExists(string UserName)
        {
            return (_context.Itinerary?.Any(e => e.UserName == UserName)).GetValueOrDefault();
        }
    }
}
