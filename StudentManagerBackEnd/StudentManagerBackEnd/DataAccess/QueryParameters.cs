using System;
using System.Collections.Generic;

namespace StudentManagerBackEnd.DataAccess
{
    public class QueryParameters
    {
        public QueryParameters(
            int page, 
            int size, 
            IEnumerable<String> sortingFields = null, 
            IEnumerable<KeyValuePair<String, String>> filteringFields = null)
        {
            Page = page;
            Size = size;
            SortingFields = sortingFields ?? new List<String>();
            FilteringFields = filteringFields ?? new List<KeyValuePair<String, String>>();
        }

        public int Page { get; }
        public int Size { get; }
        public IEnumerable<string> SortingFields { get; }
        public IEnumerable<KeyValuePair<string, string>> FilteringFields { get; }
    }
}
