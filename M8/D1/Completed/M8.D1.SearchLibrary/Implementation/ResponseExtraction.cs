using SearchLibrary.Models;
using SolrNet;
using SolrNet.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchLibrary.Implementation
{
    internal class ResponseExtraction
    {
        //Extract parts of the SolrNet response and set them in QueryResponse class
        internal void SetHeader(QueryResponse queryResponse, SolrQueryResults<Course> solrResults)
        {
            queryResponse.QueryTime = solrResults.Header.QTime;
            queryResponse.Status = solrResults.Header.Status;
            queryResponse.TotalHits = solrResults.NumFound;
        }

        internal void SetBody(QueryResponse queryResponse, SolrQueryResults<Course> solrResults)
        {
            queryResponse.Results = (List<Course>)solrResults;

            foreach (Course course in queryResponse.Results)
            {
                if (solrResults.Highlights.ContainsKey(course.CourseId))
                {
                    HighlightedSnippets snippets = solrResults.Highlights[course.CourseId];

                    if (snippets.ContainsKey("coursetitle"))
                    {
                        course.CourseTitle = snippets["coursetitle"].FirstOrDefault();
                    }

                    if (snippets.ContainsKey("description"))
                    {
                        course.CourseTitle = snippets["description"].FirstOrDefault();
                    }
                }
            }
        }

        internal void SetSpellcheck(QueryResponse queryResponse, SolrQueryResults<Course> solrResults)
        {
            List<string> spellSuggestions = new List<string>();

            foreach (SpellCheckResult spellResult in solrResults.SpellChecking)
            {
                foreach (string suggestion in spellResult.Suggestions)
                {
                    spellSuggestions.Add(suggestion);
                }
            }

            queryResponse.DidYouMean = spellSuggestions;
        }

        internal void SetFacets(QueryResponse queryResponse, SolrQueryResults<Course> solrResults)
        {
            //Authors
            if (solrResults.FacetFields.ContainsKey("author"))
            {
                queryResponse.AuthorsFacet = solrResults.FacetFields["author"].Select(facet => new KeyValuePair<string, int>(facet.Key, facet.Value)).ToList();
            }

            //
            if (solrResults.FacetFields.ContainsKey("tags"))
            {
                queryResponse.TagsFacet = solrResults.FacetFields["tags"].Select(facet => new KeyValuePair<string, int>(facet.Key, facet.Value)).ToList();
            }

            //
            if (solrResults.FacetDates.ContainsKey("releasedate"))
            {
                queryResponse.ReleaseDatesFacet = solrResults.FacetDates["releasedate"].DateResults.Select(facet => new KeyValuePair<string, int>(facet.Key.ToShortDateString(), facet.Value)).ToList();
            }

            TimeSpan ts = TimeSpan.FromSeconds( (int)solrResults.Stats["durationseconds"].Mean);

            queryResponse.AverageCourseDuration = ts.ToString(@"hh\:mm\:ss\:fff");
        }
    }
}
