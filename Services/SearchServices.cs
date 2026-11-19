using System.Net.Http.Json;
using Viola_Eliana_ReadingTracker.Models;


namespace Viola_Eliana_ReadingTracker.Services
{
    //class to find books in Open Library
    public class SearchServices
    {

        //sends messages to the internet
        private readonly HttpClient _client;

        //remembers the old searches to dont search twice for the same thing
        private readonly Dictionary<string, List<SearchResult>> _cache = new();


        public SearchServices()
        {

            _client = new HttpClient();

            //tells open library the name of the app
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("ReadingTracker/1.0");

        }

        //give it a title and get back a list of books
        public async Task<List<SearchResult>> SearchAsync(string title)
        {
            //removes extra spaces and make it lowercase
            var term = title.Trim();
            var cacheKey = term.ToLowerInvariant();

         //check if we swarch for this before,  if true uses saved list
         if(_cache.TryGetValue(cacheKey, out var cached))
            return cached;


         //make the web link with user text inside it
         var url = "https://openlibrary.org/search.json" +
                  $"?title={Uri.EscapeDataString(term)}" +
                  "&fields=key,title,author_name,first_publish_year,cover_i,edition_count" +
                  "&limit=20";

            //send the link and the change answer into a list og books
            var response = await _client.GetFromJsonAsync<SearchResponse>(url);
            var results = response?.Docs ?? new List<SearchResult>();

            //save the list for next time
            _cache[cacheKey] = results;
            return results;

        }






}

}
