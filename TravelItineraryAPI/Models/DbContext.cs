using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TravelItineraryAPI.Models;
using System.Collections.Generic;

namespace TravelItineraryAPI.Models
{
    public class TravelItineraryAPIDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public TravelItineraryAPIDBContext(DbContextOptions<TravelItineraryAPIDBContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("TravelAttractions");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Attractions> Attractions { get; set; } = null!;
        public DbSet<Itinerary> Itinerary { get; set; } = null!;

    }
}
