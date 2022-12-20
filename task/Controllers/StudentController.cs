using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task.Models;
using System.Data.Entity;
using System.Net;

//changes
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
            if (ModelState.IsValid)
            {
                db.student.Add(std);
                db.SaveChanges();

                return Home();

            }
            return View(std);
        }


        [HttpGet]
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
        }

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
                return View("Home");
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
        public ActionResult Home()
        {
            /*Student student = db.student.Find(std.StudentId);
            if (student != null)
            {
                int s = student.StudentId;
                return View(std);
            }*/
            return View("Home");
            /*
            else
            {
                return RedirectToAction("Home");
            }*/
        }

        [HttpGet]
        public ActionResult Mark()
        {
            Marks m = new Marks();
            m.Student.StudentName;
            return View();
        }

      
    }
}