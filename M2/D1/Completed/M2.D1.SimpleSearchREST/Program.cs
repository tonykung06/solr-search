using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2.D1.SimpleSearchREST.Models;

namespace M2.D1.SimpleSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://localhost:8983/solr/coursesdemo/select?q=*%3A*&wt=json&indent=true

            Console.WriteLine("Please enter query:");
            string queryFromUser = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(queryFromUser))
            {
                RestClient client = new RestClient("http://localhost:8983");
                RestRequest request = new RestRequest("/solr/coursesdemo/select");
                request.AddParameter("wt", "json");
                request.AddParameter("q", queryFromUser);
                request.AddParameter("indent", "true");

                IRestResponse queryResult = client.Execute(request);

                PrintOut(queryResult.Content.ToString());

                Console.WriteLine(Environment.NewLine + "Please enter query:");
                queryFromUser = Console.ReadLine();
            }
        }

        private static void PrintOut(string jsonResponse)
        {
            //Using Linq
            JObject jsonUsingLinq = JObject.Parse(jsonResponse);
            Console.WriteLine("Results found: " + (string)jsonUsingLinq["response"]["numFound"]);

            //Paste as JSON classes
            JsonCourses.Rootobject jsonObject = JsonConvert.DeserializeObject<JsonCourses.Rootobject>(jsonResponse);

            int i = 0;
            foreach (JsonCourses.Doc doc in jsonObject.response.docs)
            {
                Console.WriteLine(i++ + ": " + doc.coursetitle + " [" + string.Join("/", doc.author.ToArray()) + "]");
            }
        }
    }
}
