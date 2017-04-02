using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M4.D1.SolrNetConnections.Models;
using Microsoft.Practices.ServiceLocation;
using Castle.Facilities.SolrNetIntegration;
using Castle.Windsor;


namespace M4.D1.SolrNetConnections
{
    class Program
    {
        static void Main(string[] args)
        {          
            //This section uses CastleWindsor
            IWindsorContainer container = new WindsorContainer();
            SolrNetFacility solrNetFacility = new SolrNetFacility("http://localhost:8983/solr");
            solrNetFacility.AddCore("core1", typeof(Course), "http://localhost:8983/solr/gettingstarted");
            solrNetFacility.AddCore("core2", typeof(Course), "http://localhost:7574/solr/gettingstarted");
            container.AddFacility("solr", solrNetFacility);

            ISolrOperations<Course> solrCore1 = container.Resolve<ISolrOperations<Course>>("core1");
            ISolrOperations<Course> solrCore2 = container.Resolve<ISolrOperations<Course>>("core2");

            Console.WriteLine("Please enter query:");
            string queryFromUser = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(queryFromUser))
            {
                SolrQueryResults<Course> coursesCore1 = solrCore1.Query(queryFromUser);
                SolrQueryResults<Course> coursesCore2 = solrCore2.Query(queryFromUser);

                if (coursesCore1.Count == coursesCore2.Count)
                {
                    Console.WriteLine("Results from both cores match!");
                }

                int i = 0;
                foreach (Course course in coursesCore1)
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
