using SearchLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseConsoleTest
{
    internal static class PrettyPrintResults
    {
        internal static void PrintOut(QueryResponse queryResponse)
        {  
            //Print out the results
            int i = 0;
            Console.WriteLine();
            Console.WriteLine("*** Results *** ");

            foreach (Course course in queryResponse.Results)
            {
                Console.WriteLine(i++ + ": " + course.CourseTitle + " ");
                Console.WriteLine(course.Description.Substring(0, Math.Min(course.Description.Length, 200)));
                Console.Write("- by: " + string.Join(",", course.Author.ToArray()) + " ");
                Console.Write(course.ReleaseDate.ToShortDateString() + " ");
                Console.WriteLine(TimeSpan.FromSeconds(course.DurationInSeconds).ToString());
                Console.WriteLine("[" + string.Join(" ", course.Tags) + "]");
                Console.WriteLine();
            }

            Console.WriteLine("--- Results found: " + queryResponse.TotalHits);
        }
    }
}
