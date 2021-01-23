using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieAPI.APIExtern.Processors
{
    public class MovieProcessor
    {
        public static async Task<IEnumerable<MovieModel>> LoadMovie(int year, string title)
        {
            string url = "http://www.omdbapi.com/?apikey=7d7c67a8&type=movie";
            if (year > 0 && title.Length > 0)
            {
                // url = $"Movies?year={year}&title={title}";
                url += $"&s={title}&y={year}";
            }
            
            else if (year == 0 && title.Length > 0)
            {
                // url = $"Movies?title={title}";
                url += $"&s={title}";
            }
            else 
            {
                throw new Exception("You have to specify title");
            }                
         
            using (
                HttpResponseMessage response = await APIHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    // get movies from json and convert to array
                    var searchResponse = await response.Content.ReadAsAsync<SearchResponsModel>();
                    var movies = searchResponse.Search;

                    return movies;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }

        }
    }
}

