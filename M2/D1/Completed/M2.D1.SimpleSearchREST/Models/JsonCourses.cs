using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2.D1.SimpleSearchREST.Models
{
    class JsonCourses
    {
        //Paste as JSON classes
        public class Rootobject
        {
            public Responseheader responseHeader { get; set; }
            public Response response { get; set; }
        }

        public class Responseheader
        {
            public int status { get; set; }
            public int QTime { get; set; }
            public Params _params { get; set; }
        }

        public class Params
        {
            public string q { get; set; }
            public string indent { get; set; }
            public string wt { get; set; }
        }

        public class Response
        {
            public int numFound { get; set; }
            public int start { get; set; }
            public Doc[] docs { get; set; }
        }

        public class Doc
        {
            public string courseid { get; set; }
            public string coursetitle { get; set; }
            public int durationseconds { get; set; }
            public DateTime releasedate { get; set; }
            public string description { get; set; }
            public string[] tags { get; set; }
            public string tagsdescription { get; set; }
            public string[] author { get; set; }
            public string[] authorid { get; set; }
            public int fakeviews { get; set; }
            public float fakerating { get; set; }
            public long _version_ { get; set; }
        }

    }
}
