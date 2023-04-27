using System;
namespace TravelItineraryAPI.Models
{
	public class Attractions
	{
            
		public int AttractionsId { get; set; }
		public string AttractionName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

    }
}

