using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using task2.Models;
using System.Threading.Tasks;
using task.Models;
using System.Collections.Generic;
using task.Controllers;
using System.Web.Mvc;

namespace UnitTestCase
{
    [TestClass]
    public class UnitTest1
    {
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

        /* [TestMethod]
         public async Task GetAllProductsAsync_ShouldReturnAllProducts()
         {
             var testProducts = GetTestProducts();
             var controller = new staff();

             var result = await controller.getdetails();
             Assert.AreEqual(testProducts.Count, result.Count);
         }

         [TestMethod]
         public void GetProduct_ShouldReturnCorrectProduct()
         {
             var testProducts = GetTestProducts();
             var controller = new task2(testProducts);

             var result = controller.GetProduct(4) as OkNegotiatedContentResult<Product>;
             Assert.IsNotNull(result);
             Assert.AreEqual(testProducts[3].Name, result.Content.Name);
         }

         [TestMethod]
         public async Task GetProductAsync_ShouldReturnCorrectProduct()
         {
             var testProducts = GetTestProducts();
             var controller = new task2(testProducts);

             var result = await controller.GetProductAsync(4) as OkNegotiatedContentResult<Product>;
             Assert.IsNotNull(result);
             Assert.AreEqual(testProducts[3].Name, result.Content.Name);
         }

         [TestMethod]
         public void GetProduct_ShouldNotFindProduct()
         {
             var controller = new task2(GetTestProducts());

             var result = controller.GetProduct(999);
             Assert.IsInstanceOfType(result, typeof(NotFoundResult));
         }

         private List<Product> GetTestProducts()
         {
             var testProducts = new List<Product>();
             testProducts.Add(new Product { Id = 1, Name = "Demo1", Price = 1 });
             testProducts.Add(new Product { Id = 2, Name = "Demo2", Price = 3.75M });
             testProducts.Add(new Product { Id = 3, Name = "Demo3", Price = 16.99M });
             testProducts.Add(new Product { Id = 4, Name = "Demo4", Price = 11.00M });

             return testProducts;
         }*/
    }
    }

