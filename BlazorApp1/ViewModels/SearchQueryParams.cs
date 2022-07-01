using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace BlazorApp1.ViewModels
{
    public class SearchQueryParams
    {
        public string SearchText { get; set; }
        public string SortField { get; set; }
        public bool Ascending { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }

        public string BuildQueryString(string baseUri)
        {
            var uriWithQueryString = baseUri;
            var queryStringPairs = new Dictionary<string, StringValues>();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                queryStringPairs.Add("SearchText", new StringValues(SearchText));
            }
            if (!string.IsNullOrWhiteSpace(SortField))
            {
                queryStringPairs.Add("SortField", new StringValues(SortField));
            }
            queryStringPairs.Add("Ascending", new StringValues(Ascending.ToString()));
            queryStringPairs.Add("Page", new StringValues(Page.ToString()));
            queryStringPairs.Add("ItemsPerPage", new StringValues(ItemsPerPage.ToString()));

            // Build the result Uri with each value for each parameter provided
            foreach (var queryStringPair in queryStringPairs)
            {
                foreach (var queryStringValue in queryStringPair.Value)
                {
                    uriWithQueryString = QueryHelpers.AddQueryString(uriWithQueryString, queryStringPair.Key, queryStringValue);
                }
            }

            return uriWithQueryString;
        }
    }
}
