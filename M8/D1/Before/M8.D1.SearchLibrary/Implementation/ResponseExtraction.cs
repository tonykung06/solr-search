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
    }
}
