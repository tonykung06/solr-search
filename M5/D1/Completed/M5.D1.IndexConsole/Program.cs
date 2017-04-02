using M5.D1.IndexConsole.Models;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M5.D1.IndexConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Change alpharetta to either localhost or your machine name
            Startup.Init<Course>("http://localhost:8983/solr/courses");

            ISolrOperations<Course> solr = ServiceLocator.Current.GetInstance<ISolrOperations<Course>>();

            //Show Solr Admin UI and then delete all
            solr.Delete(SolrQuery.All); //Equivalent to "*:*"

            //Don't forget to commit
            solr.Commit();

            //Add one document
            //Note: create and udpate are the same
            Course courseSolr = new Course();
            courseSolr.CourseId = "enterprise-search-using-apache-solr";
            courseSolr.CourseTitle = "Getting Started with JSON in C# Using Json.NET";
            courseSolr.Author = new List<string>();
            courseSolr.Author.Add("Xavier Morera");
            courseSolr.Tags = new List<string>();
            courseSolr.Tags.Add("solr");
            courseSolr.Tags.Add("big-data");
            courseSolr.Description = "Search is one of the most misunderstood functionalities in the IT industry. Even further, Enterprise Search used to be neither for the faint of heart, nor for those with a thin wallet. However, since the introduction of Apache Solr, the name of the game has changed. Apache Solr brings high quality Enterprise Search to the masses. Don?t leave home without it!";

            solr.Add(courseSolr);

            solr.Commit();

            //Add a couple of documents in a single command
            List<Course> courses = new List<Course>();

            Course courseGit = new Course();
            courseGit.CourseId = "using-git-with-gui";
            courseGit.CourseTitle = "Getting Started with JSON in C# Using Json.NET";
            courseGit.Author = new List<string>();
            courseGit.Author.Add("Xavier Morera");
            courseGit.Tags = new List<string>();
            courseGit.Tags.Add("json");
            courseGit.Tags.Add("microsoft");
            courseGit.Tags.Add("javascript");
            courseGit.Description = "There is no doubt Git is taking over the world of source control management, mainly in open source but growing rapidly in enterprise as well. The problem usually lies in that Git is not for the faint of heart as it comes with a steep learning curve - it is hard to get started. In this course, I will help you smooth out the learning curve, understand Git?s mechanics and branching model, and in general be productive much quicker with the aid of a great free tool: Atlassian SourceTree.";

            courses.Add(courseGit);

            Course courseJson = new Course();
            courseJson.CourseId = "json-csharp-jsondotnet-getting-started";
            courseJson.CourseTitle = "Using Git with a GUI";
            courseJson.Author = new List<string>();
            courseJson.Author.Add("Xavier Morera");
            courseJson.Tags = new List<string>();
            courseJson.Tags.Add("git");
            courseJson.Tags.Add("source-control");
            courseJson.Description = "The rising popularity of the web, mainly around JavaScript related technologies, has given JSON a great deal of importance over other data interchange formats, like XML. JSON is a lightweight, human-readable, efficient, and easy to understand data interchange format. JSON stands for JavaScript Object Notation. If you are a C# or .NET developer who needs to work with JSON, or even understand what JSON is, then this course is for you. In this course, we will learn how to serialize, deserialize, use LINQ To JSON, improve serialization performance, BSON, create schemas, validate JSON, and, in general, work with Json.NET, a popular, high performance, free, and open source JSON framework for .NET.";

            courses.Add(courseJson);

            solr.AddRange(courses);
            solr.Commit();
            
           //How to do an edit
            courseSolr.CourseId = "enterprise-search-using-apache-solr";
            courseSolr.CourseTitle = "edited";
            courseSolr.Author = new List<string>();
            courseSolr.Author.Add("edited");
            courseSolr.Tags = new List<string>();
            courseSolr.Tags.Add("edited");
            courseSolr.Tags.Add("edited");
            courseSolr.Description = "edited";

            solr.Add(courseSolr);
            solr.Commit();

            ////Learn how to delete too
            //solr.Delete(courseJson);
            //solr.Commit();
            
            ////Test Rollback too!
            //solr.Delete(courseSolr);
            //solr.Rollback();
            //solr.Commit();
        }
    }
}
