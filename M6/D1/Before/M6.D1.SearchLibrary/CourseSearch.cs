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
        }

        public QueryResponse DoSearch(CourseQuery query)
        {
            throw new NotImplementedException();
            //Create an object to hold results
            
            //Echo back the original query                       

            //Get a connection

            //Execute the query

           //Set response

            //Return response;
        }
    }
}