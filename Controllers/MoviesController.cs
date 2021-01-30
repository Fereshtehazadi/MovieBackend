using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.APIExtern.Processors;
using Microsoft.Extensions.Caching.Memory;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        
        public MoviesController(IMemoryCache cache)
        {
            _cache = cache;
           
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IEnumerable<MovieModel>> GetMovies(string title,int year)
        {        
            var movies = await _cache.GetOrCreateAsync($"title={title}, year={year}", async cacheEntry => 
            {
                cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(60);
                return await MovieProcessor.LoadMovie(year, title);
            });                                    

            return movies;
        }

      

      

      
    }
}
