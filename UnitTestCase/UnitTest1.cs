using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using task2.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using task.Models;
using System.Collections.Generic;
using task.Controllers;
using System.Web.Mvc;
using task2.Controllers;
using Newtonsoft.Json;
using task2.Services;
using Xunit;

namespace UnitTestCase
{
    [TestClass]
    public class UnitTest1
    {
        /*public dynamic PostStaff(staff staff)
        {
            using (StudentEntities db = new StudentEntities())
            {
                db.staffs.Add(staff);
                db.SaveChanges();
            }
            return staff;
        }*/

        
        [TestMethod]
        [DataRow("vinod",1,"kovai")]
        public void PostStaff(string name,int id,string address)
        {
            // Arrange
            var methods = new Methods();
            staff staff = new staff()
            {
                Name = name,
                ID = id,
                Address = address
            };
            // Act
            var response = staff;

                // Assert
                
                Assert.IsNotNull(response);
        }

        [TestMethod]
        [DataRow(null, null)]
        public void PostStaff2(string name,  string address)
        {
            // Arrange
            staff staff = new staff()
            {
                Name = name,
                Address = address
            };
            // Act
            var response = staff;

            // Assert

            Assert.AreEqual(null,response.Name);
            
            Assert.AreEqual(null, response.Address);
        }

        [TestMethod]
        [DataRow(null, null)]
        public void PostStaff3(string name, string address)
        {
            // Arrange
            staff staff = new staff()
            {
                Name = name,
                Address = address
            };
            // Act
            var response = staff;

            // Assert

            Assert.IsNull(response.Name);

            Assert.IsNull(response.Address);
        }

        [TestMethod]
        [DataRow(null, null, null)]
        public void TestPutMethod(int id,string name, string password)
        {
            
            var staff = new staff { ID = id, Name = name, Password = password};
            //
            
            var controller = new Methods();
            var result = staff;
            Assert.IsNull(result);
        }


        [TestMethod]
        public void GetStaff()
        {
            // Arrange
            Methods staffController = new Methods();

            // Act
            var staff1 = new staff { ID = 2, Name = "Raj", Password = "12345", DateOfJoin = DateTime.Now };
            var response = staffController.GetStaff(staff1);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count > 0);
        }


        [TestMethod]
        public void Signin_ReturnsHome()
        {
            // Arrange
            var controller = new StudentController();
            var student = new Student { Username = "John", Password = "password" };

            // Act
            var result = controller.signin(student);

            // Assert

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void IndexReturnsView()
        {
            // Arrange
            var controller = new StudentController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Add_Post_Test()
        {
            var controller = new StudentController();
            var std = new Student()
            {
                StudentId = 1,
                StudentName = "Test Name",
                Address = "Street1",
                Email = "test@gmail.com"
            };

            var result = controller.Home(std.StudentId) as ViewResult;
            Assert.IsNotNull(result);
        }


    }
}

