using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace searchqueryapp.Data.model
{
    public class Query
    {
        public string searchText { get; set; }

        public string select { get; set; }

        public string searchMode { get; set; }

        public string searchFields { get; set; }

        public string scoringProfile { get; set; }

        public string scoringParameters { get; set; }

        public string queryType { get; set; }

        public string orderBy { get; set; }

        public string minimumCoverage { get; set; }

        public string highlightFields { get; set; }

        public string highlightPreTag { get; set; }

        public string highlightPostTag { get; set; }

        public string filter { get; set; }

        public string facets { get; set; }

        public string includeTotalResultCount { get; set; }

        public string skip { get; set; }

        public string top { get; set; }

        public string scoringStatictics { get; set; }

        public string apiVersion { get; set; }

        public string indexName { get; set; }

        public string featuresMode { get; set; }
    }
}
