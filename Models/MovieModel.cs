using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Models
{
    public class MovieModel
    {
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public int Year{ get; set; }
        //public string Actors{ get; set; }
        public string Poster { get; set; }
    }
}
