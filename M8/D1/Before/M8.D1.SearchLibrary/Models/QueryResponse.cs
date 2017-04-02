using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchLibrary.Models
{
    public class QueryResponse
    {
        public QueryResponse()
        {
            //Initialize properties
        }

        //Expose properties that will be returned to from the search library
        public List<Course> Results { get; set; }

        public int TotalHits { get; set; }

        public int QueryTime { get; set; }

        public int Status { get; set; }

        public CourseQuery OriginalQuery { get; set; }

        public List<string> DidYouMean { get; set; }
    }
}