using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using task.Models;

namespace task.Models
{

    public class Student
    {

        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Student Name is Required.")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Student Address is Required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Student Email is Required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
    

    



