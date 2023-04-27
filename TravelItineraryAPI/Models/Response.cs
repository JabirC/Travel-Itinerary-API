using System;
namespace TravelItineraryAPI.Models
{
	public class Response
	{
		public int statusCode { get; set; }
		public string statusDescription { get; set; }
        public IEnumerable<Attractions> data { get; set; }
	}
}

