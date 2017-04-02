using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchLibrary.Models
{
    public class CourseQuery
    {
        public CourseQuery()
        {
            Rows = 10;
            Start = 0;
        }
        //Query object that holds parameters sent to the search library
        public string Query { get; set; }

        public int Start { get; set; }

        public int Rows { get; set; }
    }
}