using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MovieAPI.APIExtern
{
    public static class APIHelper
    {
      
            public static HttpClient ApiClient { get; set; }

            public static void InitializeClient()
            {
                ApiClient = new HttpClient();
               // ApiClient.BaseAddress = new Uri("http://www.omdbapi.com/?apikey=7d7c67a8&type=movie"); // Address till API extern
                ApiClient.DefaultRequestHeaders.Accept.Clear();
                ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

        
    }
}
