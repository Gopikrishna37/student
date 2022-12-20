using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using task.Models;

namespace task.Models
{
    public class DBstudent: DbContext
    {
        public DbSet<Student> student { get; set; }
        public DbSet<Marks> marks { get; set; }
        public DbSet<Subject> subject { get; set; }
       
    }
}