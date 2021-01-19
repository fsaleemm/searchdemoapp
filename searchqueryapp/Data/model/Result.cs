using Azure.Search.Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace searchqueryapp.Data.model
{
    public class Result
    {
        //public DocumentSearchResult<Incident> resultList;
        public List<Dictionary<string, string>> processedResults;
        public Dictionary<string, List<FacetResult>> facets;
        public string error;
        public long? count;
    }
}
