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
            if (queryResponse.Results.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("*** No results found: ***");
                if (queryResponse.DidYouMean.Count > 0)
                {
                    Console.WriteLine("Did you mean?: " + string.Join(" / ", queryResponse.DidYouMean) + Environment.NewLine);
                }

                return;
            }

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

            Console.WriteLine();
            Console.WriteLine("Facets");

            Console.WriteLine("- Authors");
            foreach (KeyValuePair<string, int> author in queryResponse.AuthorsFacet)
            {
                Console.WriteLine(" " + author.Key + " (" + author.Value + ")");
            }

            Console.WriteLine();

            Console.WriteLine("- Tags");
            foreach (KeyValuePair<string, int> tag in queryResponse.TagsFacet)
            {
                Console.WriteLine(" " + tag.Key + " (" + tag.Value + ")");
            }
            Console.WriteLine();

            Console.WriteLine("- Release Dates");
            foreach (KeyValuePair<string, int> releaseDate in queryResponse.ReleaseDatesFacet)
            {
                Console.WriteLine(" " + releaseDate.Key + " (" + releaseDate.Value + ")");
            }
            Console.WriteLine();

            Console.WriteLine("Average course duration" + queryResponse.AverageCourseDuration);

            Console.WriteLine("--- Results found: " + queryResponse.TotalHits);
        }
    }
}
