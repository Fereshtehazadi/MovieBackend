

using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieApi.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<MovieModel> Movies{ get; set; }
    }
}