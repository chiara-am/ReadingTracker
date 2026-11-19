using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Viola_Eliana_ReadingTracker.Models
{

    //one book returned by the open library search.
    // this is temporary data. it is replaced every time the user searches
    public class SearchResult
    {
        // fields that come from open library
        // jsonProperyName links the name in the JSON to the property name here

        //book id in openlibrary
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        // the ?  means " this is allowed to be empty"
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        // a list, beacuse a book can have more than one author
        [JsonPropertyName("author_name")]
        public List<string>? AuthorNames { get; set; }

        [JsonPropertyName("first_publish_year")]
        public int? FirstPublishYear { get; set; }

        // number used to build the cover image link
        [JsonPropertyName("cover_i")]
        public int? CoverId { get; set; }

        [JsonPropertyName("edition_count")]
        public int? EditionCount { get; set; }

        // properties made for the screen
        // not in json so JSONIgnore tells c# to skip them 
        // replacing missing data with a friendly value, so xaml never shows blanks

        [JsonIgnore]
        //shows "untitled" if the ttle is missing
        public string DisplayTitle =>
            string.IsNullOrWhiteSpace(Title) ? "Untitled" : Title;

        [JsonIgnore]
        //joins all authors with commas, or shows "unkown author" if there is none
        public string DisplayAuthor =>
            AuthorNames is { Count: > 0 } ? string.Join(", ", AuthorNames) : "Unknown author";

        //turns the year into text, ir shows "year unkown" if it is missing
        [JsonIgnore]
        public string DisplayYear =>
            FirstPublishYear?.ToString() ?? "Year unknown";


        //builds the cover image link, or uses the placeholder image if there is no cover
        [JsonIgnore]
        public string CoverUrl =>
            CoverId is null
                ? "placeholder_cover.png"
                : $"https://covers.openlibrary.org/b/id/{CoverId}-M.jpg";
    }

    //open the library
    public class SearchResponse
    {
        [JsonPropertyName("docs")]
        public List<SearchResult> Docs { get; set; } = new();
    }
}
