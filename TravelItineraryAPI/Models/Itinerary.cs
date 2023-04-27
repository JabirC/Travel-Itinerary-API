﻿using System;
namespace TravelItineraryAPI.Models
{
	public class Itinerary
	{
        public int ItineraryId { get; set; }
        public string UserName { get; set; }
        public Nullable<int> Location_1 { get; set; }
        public Nullable<int> Location_2 { get; set; }
        public Nullable<int> Location_3 { get; set; }
        public Nullable<int> Location_4 { get; set; }
    }
}

