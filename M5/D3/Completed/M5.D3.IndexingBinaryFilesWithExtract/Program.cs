using M5.D3.IndexingBinaryFilesWithExtract.Models;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M5.D3.IndexingBinaryFilesWithExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialization
            //Make sure you have an extractdemo core. If not, simply run 'bin\solr.cmd create -c extractdemo'
            //In this particular case we are using a data driven schema, so no need to define a schema.xml
            //Change alpharetta to either localhost or your machine name
            Startup.Init<Document>("http://localhost:8983/solr/extractdemo");
            ISolrOperations<Document> solr = ServiceLocator.Current.GetInstance<ISolrOperations<Document>>();

            //Delete to confirm that new documents are indexed
            solr.Delete(SolrQuery.All);
            solr.Commit();

            //Index documents. 
            foreach (string filename in Directory.GetFiles(@"..\..\SourceFiles"))
            {
                using (FileStream fileStream = File.OpenRead(filename))
                {
                    var response = solr.Extract(new ExtractParameters(fileStream, System.IO.Path.GetFileName(filename))
                          {
                              ExtractFormat = ExtractFormat.Text,
                              ExtractOnly = false
                          });
                }
            }

            solr.Commit();
            //And now the important part, run a search in the Admin UI to confirm
            //A good example is search for 'settings attributes' which returns '3-json-csharp-jsondotnet-getting-started-m3-slides.pdf'
        }
    }
}
