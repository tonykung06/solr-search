﻿using M4.D1.SolrNetConnections.Models;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4.D1.SolrNetConnections
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.Init<Course>("http://localhost:8983/solr/coursesdemo");

            ISolrOperations<Course> solr = ServiceLocator.Current.GetInstance<ISolrOperations<Course>>();

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