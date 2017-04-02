using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchLibrary.Models;
using SearchLibrary.Implementation;
using SolrNet.Commands.Parameters;
using SolrNet;

namespace SearchLibrary
{
    public class CourseSearch
    {
        private Connection connection;

        public CourseSearch()
        {
            //Initialize connection
            //Connection can be initialized once and then retrieved via ServiceLocator.GetInstance
            //But we are creating it for every search library instantiation for demo purposes            
            connection = new Connection("http://alpharetta:8983/solr/courses");
        }

        public QueryResponse DoSearch(CourseQuery query)
        {
            //Create an object to hold results
            SolrQueryResults<Course> solrResults;
            QueryResponse queryResponse = new QueryResponse();
            
            //Echo back the original query 
            queryResponse.OriginalQuery = query;          

            //Get a connection
            ISolrOperations<Course> solr = connection.GetSolrInstance();
            QueryOptions queryOptions = new QueryOptions
            {
                Rows = query.Rows,
                StartOrCursor = new StartOrCursor.Start(query.Start)
            };

            //Execute the query
            ISolrQuery solrQuery = new SolrQuery(query.Query);

            solrResults = solr.Query(solrQuery, queryOptions);

           //Set response
            ResponseExtraction extractResponse = new ResponseExtraction();

            extractResponse.SetHeader(queryResponse, solrResults);
            extractResponse.SetBody(queryResponse, solrResults);

            //Return response;
            return queryResponse;
        }
    }
}