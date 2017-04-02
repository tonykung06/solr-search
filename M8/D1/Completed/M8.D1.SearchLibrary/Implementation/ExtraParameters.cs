using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchLibrary.Implementation
{
    internal class ExtraParameters
    {
        internal List<KeyValuePair<string, string>> BuildExtraParameters()
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();

            parameters.Add(new KeyValuePair<string, string>("hl.simple.pre","<b>"));
            parameters.Add(new KeyValuePair<string, string>("hl.simple.post", "</b>"));

            parameters.Add(new KeyValuePair<string, string>("defType", "edismax"));
            parameters.Add(new KeyValuePair<string, string>("qf", "coursetitle^10.0 description^1.0 tags^6.0 _text_"));

            return parameters;
        }
    }
}
