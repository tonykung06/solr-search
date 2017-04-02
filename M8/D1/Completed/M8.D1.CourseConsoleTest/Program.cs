using SearchLibrary;
using SearchLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //create a search library instance
            CourseSearch searchLibrary = new CourseSearch();

            CourseQuery query = new CourseQuery();

            query = QueryReader.ReadQuery();

            while (query.Query != Environment.NewLine)
            {
                QueryResponse response = searchLibrary.DoSearch(query);

                PrettyPrintResults.PrintOut(response);

                query = QueryReader.ReadQuery();
            }
        }
    }
}
