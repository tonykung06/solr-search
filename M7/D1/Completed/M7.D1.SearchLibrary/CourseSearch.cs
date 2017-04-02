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

            //A boost query can be applied using the bq parameters below
            //http://localhost:8983/solr/courses/select?q=animation%0A&rows=100&fl=coursetitle+tags&wt=json&indent=true&defType=edismax&bq=tags%3Ajavascript%5E10.0&stopwords=true&lowercaseOperators=true

            //A boost function can be applied using the bf parameters below
            //http://localhost:8983/solr/courses/select?q=animation&fl=coursetitle+releasedate+tags&wt=json&indent=true&defType=edismax&bq=tags%3Ajavascript%5E10.0&bf=recip(ms(NOW%2Creleasedate)%2C3.16e-11%2C1%2C1)%5E10.0&stopwords=true&lowercaseOperators=true

            //Query used to test stemming
            //http://localhost:8983/solr/courses/select?q=get+AND+json&wt=json&indent=true

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