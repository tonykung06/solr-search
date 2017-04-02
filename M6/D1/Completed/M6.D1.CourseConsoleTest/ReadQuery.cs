using SearchLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseConsoleTest
{
    internal static class QueryReader
    {
        internal static CourseQuery ReadQuery()
        {
            //Create a query object
            CourseQuery query = new CourseQuery();

            Console.WriteLine("Please enter your query");

            //Read the query
            query.Query = Console.ReadLine();

            //return the query object 
            return query;
        }
    }
}
