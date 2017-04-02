using SolrNet.Commands.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchLibrary.Implementation
{
    internal class Highlights
    {
        internal HighlightingParameters BuildHighlightParameters()
        {
            HighlightingParameters parameters = new HighlightingParameters()
            {
                Fields = new List<string>() { "coursetitle", "description" },
                Fragsize = 200
            };

            return parameters;
        }
    }
}
