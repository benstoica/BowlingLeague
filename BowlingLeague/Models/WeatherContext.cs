using Microsoft.EntityFrameworkCore;

namespace BowlingLeague.Models
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        { 
        }

        public DbSet<WeatherForecast> Forecasts { get; set; } = null!;
    }
}
