using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Facilities.SolrNetIntegration;
using Castle.Windsor;
using M4.D2.SolrFromCSharp.Models;
using SolrNet;

namespace M4.D2.SolrFromCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Startup.Init<Course>("http://alpharetta:8983/solr/coursesdemo");
            //ISolrOperations<Course> solr = ServiceLocator.Current.GetInstance<ISolrOperations<Course>>();

            IWindsorContainer container = new WindsorContainer();
            SolrNetFacility solrNetFacility = new SolrNetFacility("http://alpharetta:8983/solr");
            solrNetFacility.AddCore(typeof(Course), "http://alpharetta:8983/solr/coursesdemo");
            container.AddFacility("solr", solrNetFacility);

            ISolrOperations<Course> solr = container.Resolve<ISolrOperations<Course>>();

            Console.WriteLine("Please enter query:");
            string queryFromUser = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(queryFromUser))
            {
                SolrQueryResults<Course> courses = solr.Query(queryFromUser);

                int i = 0;
                foreach (Course course in courses)
                {
                    Console.WriteLine(i++ + ": " + course.CourseTitle + " [" + string.Join("/", course.Author.ToArray()) + "]");
                }

                Console.WriteLine(Environment.NewLine + "Please enter query:");
                queryFromUser = Console.ReadLine();
            }

            Console.WriteLine(Environment.NewLine + "Demo completed!");
        }
    }
}
