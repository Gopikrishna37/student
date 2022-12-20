using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using task.Models;
using System.ComponentModel.DataAnnotations;

namespace task.Models
{
  public  class Subject
    {
        [Key]
        public   int CourseId { get; set; }
        public string CoursetName { get; set; }
        public  string Code { get; set; }
    }

}