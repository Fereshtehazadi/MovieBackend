using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using MovieApi.Models;
using MovieAPI.DTOs;
using MovieAPI.APIExtern.Processors;
using Microsoft.Extensions.Caching.Memory;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMemoryCache _cache;
        private readonly MovieContext _context;

        public MoviesController(MovieContext context, IMemoryCache cache)
        {
            _cache = cache;
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IEnumerable<MovieModel>> GetMovies(string title,int year)
        {
            //var movies = await MovieProcessor.LoadMovie(year, title);
            var movies = await _cache.GetOrCreateAsync($"title={title}, year={year}", async cacheEntry => 
            {
                cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(60);
                return await MovieProcessor.LoadMovie(year, title);
            });                                    

            return movies;
        }

        // GET: api/Movies/5
        [HttpGet("{ImdbId}")]
        public async Task<ActionResult<MovieModel>> GetMovie(string imdbId)
        {
            var movie = await _context.Movies.FindAsync(imdbId);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMovie(long id, MovieModel movie)
        //{
        //    if (id != movie.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(movie).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<MovieModel>> PostMovie(MovieModel movie)
        //{
        //    _context.Movies.Add(movie);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof (GetMovie), new { id = movie.Id }, movie);
            
        //}

        // DELETE: api/Movies/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<MovieModel>> DeleteMovie(long id)
        //{
        //    var movie = await _context.Movies.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Movies.Remove(movie);
        //    await _context.SaveChangesAsync();

        //    return movie;
        //}

        //private bool MovieExists(long id)
        //{
        //    return _context.Movies.Any(e => e.Id == id);
        //}
    }
}
