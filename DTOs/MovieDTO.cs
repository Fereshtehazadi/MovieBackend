using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.DTOs
{
    public class MovieDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Actors { get; set; }
        public string Poster { get; set; }
    }
}
