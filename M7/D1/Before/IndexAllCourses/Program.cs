using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using SolrNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndexAllCourses.Models;

namespace IndexAllCourses
{
    class Program
    {
        static void Main(string[] args)
        {
            //In case you want to delete courses before starting this demo run the following command from a browser
            //http://localhost:8983/solr/courses/update?stream.body=<delete><query>*:*</query></delete>&commit=true

            //Read file and deserialize using Json.Net (or feel free to use any other method)
            List<Course> allCourses = JsonConvert.DeserializeObject<List<Course>>(File.ReadAllText(@"..\..\courses.json"));

            Startup.Init<Course>("http://alpharetta:8983/solr/courses");

            ISolrOperations<Course> solr = ServiceLocator.Current.GetInstance<ISolrOperations<Course>>();

            foreach (Course course in allCourses)
            {
                solr.Add(course);
            }

            solr.Commit();       
        }
    }
}
