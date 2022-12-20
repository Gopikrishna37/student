using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using task.Models;

namespace task.Models
{
    public class Marks 
    {

        [Key]
        public int ID { get; set; }
        public int SubjectId { get; set; }
        public int Score { get; set; }
       public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student
        {
            get; set;
        }
    }
}