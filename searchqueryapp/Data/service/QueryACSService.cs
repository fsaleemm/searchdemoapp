using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using searchqueryapp.Data.model;
using Microsoft.Extensions.Configuration;

namespace searchqueryapp.Data.service
{
    public class QueryACSService
    {
        private static IConfiguration _configuration;

        public QueryACSService(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public Result GetResults(Query searchQuery)
        {
            Uri searchEndpoint = new Uri(_configuration["SearchServiceEndpoint"]);
            string apiKey = _configuration["SearchServiceQueryApiKey"];
            string indexName = _configuration["SearchIndexName"];
            Result results = new Result();
            List<Dictionary<string, string>> processedResults = new List<Dictionary<string, string>>();
            Dictionary<string, List<FacetResult>> facetResults = new Dictionary<string, List<FacetResult>>();

            Dictionary<string, string> speakersDict = _configuration.GetSection("Speakers").GetChildren().ToDictionary(x => x.Key, x => x.Value);


            SearchOptions options = new SearchOptions
            {
                Size = 100,
                IncludeTotalCount = true,
                Facets = { "duration,sort:count", "recognizedPhrases/speaker,sort:count" },
                HighlightFields = { "combinedRecognizedPhrases/display" },
                HighlightPreTag = "<span class='hitHighlight'>",
                HighlightPostTag = "</span>",
                Filter = searchQuery.filter,
                SearchMode = SearchMode.All
            };

            AzureKeyCredential credential = new AzureKeyCredential(apiKey);

            SearchClient client = new SearchClient(searchEndpoint, indexName, credential);

            SearchResults<SearchDocument> response = client.Search<SearchDocument>(searchQuery.searchText, options);


            foreach (SearchResult<SearchDocument> result in response.GetResults())
            {
                SearchDocument doc = result.Document;
                Dictionary<string, string> r = new Dictionary<string, string>();
                string speakerTranscription = string.Empty;

                r.Add("filename", (string)doc["metadata_storage_name"]);
                r.Add("timestamp", (string)doc["timeStamp"]);
                //results.processedResults.Add("size", (string)doc["metadata_storage_size"]);
                r.Add("lastmodified", (string)doc["metadata_storage_last_modified"]);
                r.Add("transcription", (string)doc.GetObjectCollection("combinedRecognizedPhrases")[0]["display"]);

                foreach (SearchDocument recognizedPhrase in doc.GetObjectCollection("recognizedPhrases"))
                {
                    speakerTranscription += String.Format("<span class=\"speakerLabel\">Speaker {0}</span>: {1} <br>", recognizedPhrase.GetInt32("speaker").ToString(), recognizedPhrase.GetObjectCollection("nBest")[0].GetString("display"));
                }

                r.Add("speakerTranscription", speakerTranscription);

                if (result.Highlights != null)
                    r.Add("highlights", String.Join(" ...  ", result.Highlights["combinedRecognizedPhrases/display"].ToList()));
                else
                    r.Add("highlights", r["transcription"].Substring(0, Math.Min(r["transcription"].Length, 200)) + "...");

                    r.Add("source", (string)doc["source"]);

                processedResults.Add(r);
            }

            foreach ( var f in response.Facets)
            {
                switch (f.Key) {
                    case "recognizedPhrases/speaker":
                        facetResults["recognizedPhrases/speaker"] = f.Value.ToList();
                        continue;

                    case "duration":
                        facetResults["duration"] = f.Value.ToList();
                        continue;
                }
            }

            results.processedResults = processedResults;
            results.facets = facetResults;
            results.count = response.TotalCount;

            return results;
        }
    }
}
