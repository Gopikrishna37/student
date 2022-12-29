using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task.Models
{
    public class studentdto
    {

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<Markdto> Marks { get; set; }
        public int Total { get; set; }

    }
    public class Markdto
    {
        public string subjectname { get; set; }
        public int marks { get; set; }
    }
}
