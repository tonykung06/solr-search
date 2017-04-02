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

            Console.WriteLine("Do you want to specify an author?");
            string author = Console.ReadLine();

            while (!string.IsNullOrEmpty(author))
            {
                query.AuthorFilter.Add(author);

                Console.WriteLine("Add another author?");
                author = Console.ReadLine();
            }

            Console.WriteLine("Do you want to specify a tag?");
            string tag = Console.ReadLine();
            while (!string.IsNullOrEmpty(tag))
            {
                query.TagFilter.Add(tag);

                Console.WriteLine("Add another tag?");
                tag = Console.ReadLine();
            }

            //return the query object 
            return query;
        }
    }
}
