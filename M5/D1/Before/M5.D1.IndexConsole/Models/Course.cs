using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M5.D1.IndexConsole.Models
{
    public class Course
    {
        [SolrUniqueKey("courseid")]
        public string CourseId { get; set; }

        [SolrField("coursetitle")]
        public string CourseTitle { get; set; }

        //Add all other fields here
    }
}
