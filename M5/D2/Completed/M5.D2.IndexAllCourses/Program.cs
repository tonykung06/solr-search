using M5.D2.IndexAllCourses.Models;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using SolrNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexAllCourses
{
    class Program
    {
        static void Main(string[] args)
        {
            //Read file and deserialize using Json.Net (or feel free to use any other method)
            List<Course> allCourses = JsonConvert.DeserializeObject<List<Course>>(File.ReadAllText(@"..\..\courses.json"));

            //Change alpharetta to either localhost or your machine name
            Startup.Init<Course>("http://localhost:8983/solr/courses");

            ISolrOperations<Course> solr = ServiceLocator.Current.GetInstance<ISolrOperations<Course>>();

            //Delete courses before starting this demo
            //http://localhost:8983/solr/courses/update?stream.body=<delete><query>*:*</query></delete>&commit=true
            solr.Delete(SolrQuery.All); //Equivalent to "*:*"
            solr.Commit();

            foreach (Course course in allCourses)
            {
                solr.Add(course);
            }

            solr.Commit();       
        }
    }
}
