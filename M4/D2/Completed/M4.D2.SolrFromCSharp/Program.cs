using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Facilities.SolrNetIntegration;
using Castle.Windsor;
using M4.D2.SolrFromCSharp.Models;
using SolrNet;
using SolrNet.Commands.Parameters;

namespace M4.D2.SolrFromCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Startup.Init<Course>("http://alpharetta:8983/solr/coursesdemo");
            //ISolrOperations<Course> solr = ServiceLocator.Current.GetInstance<ISolrOperations<Course>>();

            IWindsorContainer container = new WindsorContainer();
            //Change alpharetta to either localhost or your machine name
            SolrNetFacility solrNetFacility = new SolrNetFacility("http://alpharetta:8983/solr");
            solrNetFacility.AddCore(typeof(Course), "http://alpharetta:8983/solr/coursesdemo");
            container.AddFacility("solr", solrNetFacility);

            ISolrOperations<Course> solr = container.Resolve<ISolrOperations<Course>>();

            Console.WriteLine("Please enter query:");
            string queryFromUser = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(queryFromUser))
            {
                Console.WriteLine("Please specify author or enter for all: ");
                string author;
                ICollection<ISolrQuery> filters = new List<ISolrQuery>();
                author = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(author))
                {
                    filters.Add(new SolrQueryByField("author", author));
                }

                FacetParameters facets = new FacetParameters
                {
                    Queries = new List<ISolrFacetQuery>{
                new SolrFacetFieldQuery("author"){MinCount = 1}
                }
                };

                QueryOptions queryOptions = new QueryOptions()
                {
                    StartOrCursor = new StartOrCursor.Start(0),
                    Rows = 100,
                    FilterQueries = filters,
                    Facet = facets
                };

                SolrQueryResults<Course> courses = solr.Query(queryFromUser, queryOptions);
               
                Console.WriteLine();
                Console.WriteLine("*Results*");
                Console.WriteLine("Total found: " + courses.NumFound);

                int i = 0;
                foreach (Course course in courses)
                {
                    Console.WriteLine(i++ + ": " + course.CourseTitle + " [" + string.Join("/", course.Author.ToArray()) + "]");
                }

                Console.WriteLine();
                Console.WriteLine("- Author facet: ");
                ICollection<KeyValuePair<string, int>> authorFacet = courses.FacetFields["author"];

                foreach (KeyValuePair<string, int> f in authorFacet)
                {
                    Console.WriteLine(i++ + ": " + f.Key + " (" + f.Value + ")");
                }

                Console.Write("Execution time (ms): " + courses.Header.QTime);

                Console.WriteLine(Environment.NewLine + "Please enter query:");
                queryFromUser = Console.ReadLine();
            }

            Console.WriteLine(Environment.NewLine + "Demo completed!");
        }
    }
}
