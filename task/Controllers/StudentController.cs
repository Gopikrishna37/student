﻿using System.Linq;
using System.Web.Mvc;
using task.Models;
using System.Data.Entity;
using System.Net;
using System;
using task.Service;
//changes
//checking git status
namespace task.Controllers
{
    
    public class StudentController : Controller
    { 

        /*   public static List<Student> studentList = new List<Student>{
               new Student() { StudentId = 1, StudentName = "John", Address = "chennai" } ,
               new Student() { StudentId = 2, StudentName = "Steve",  Address = "delhi"} ,
               new Student() { StudentId = 3, StudentName = "Bill",  Address = "bengalore" } ,
               new Student() { StudentId = 4, StudentName = "Ram" , Address = "hydrabad" } ,
               new Student() { StudentId = 5, StudentName = "Ron" , Address = "madurai" } ,
               new Student() { StudentId = 6, StudentName = "Chris" , Address = "vellore" } ,
               new Student() { StudentId = 7, StudentName = "Rob" , Address = "chennai"}
           };*/
       public DBstudent db = new DBstudent();
        [SessionTimeout]
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.student.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Student std)
        {
            Mail m = new Mail();
            if (ModelState.IsValid)
            {

                db.student.Add(std);
                db.SaveChanges();
                string mail=m.Sender(std.Email);
                return Home(std.StudentId);

            }
            return View(std);
        }

        
        [HttpGet]
        [SessionTimeout]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.student.Find(Id);
            if(student==null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        
        [HttpPost]
        [SessionTimeout]
        public ActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                db.Entry(std).State=EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(std);
        }
        /*
                [HttpGet]
                public ActionResult Delete(int? Id)
                {
                    if (Id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Student student = db.student.Find(Id);
                    if (student == null)
                    {
                        return HttpNotFound();
                    }
                    return View(student);
                }*/
        
        [HttpPost]
        public ActionResult Delete(int Id)
        {
                Student student = db.student.Find(Id);
                db.student.Remove(student);
                db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        
       
        public ActionResult signin()
        {

            return View("signin");
        }

        [HttpPost]

        public ActionResult signin(Student std)
        {
            var obj = db.student.Where(a => a.Username.Equals(std.Username) && a.Password.Equals(std.Password)).FirstOrDefault();
            if (obj != null)
            {
                Session["StudentId"] = obj.StudentId;
                Session["StudentName"] = obj.StudentName;
                return Home(obj.StudentId);
            }
            else
            {
                //action = "ErrorPage"; controller = "UnAutherized";      
                ViewBag.Message = "UserName or password is wrong";
                TempData["strLoginErrorInfo"] = "Invalid Username or Password";
                TempData.Keep();
                return RedirectToAction("signin");
            }
           
        }
        
        [HttpGet]
        [SessionTimeout]
        public ActionResult Home(int? id)
        {

            Student student = db.student.Find(id);
            if (student != null)
            {
                
                return View("Home",student);
            }
            return View();
        }
        [SessionTimeout]
        [HttpGet]
        public ActionResult Mark()
        {
            var result = from ma in db.marks
                         join st in db.student on ma.StudentId equals st.StudentId
                         join sub in db.subject on ma.SubjectId equals sub.CourseId
                         where ma.StudentId==2
                         select new  {
                             st.StudentId,
                             st.StudentName,
                             sub.CoursetName,
                             ma.Score
                         };

            var result1 = result.Select(_ => new studentdto
            {
                StudentId = _.StudentId,
                StudentName = _.StudentName,
                Marks = result.Select(m => new Markdto()
                {
                    subjectname = m.CoursetName,
                    marks = m.Score,
                }).ToList(),
                Total=result.Sum(m=>m.Score)
             }).FirstOrDefault();

                return View(result1);

        }

      
    }
}