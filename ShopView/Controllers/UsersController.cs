using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using DBcontext.DBModels;
using DBcontext.ServiceModels;
using Newtonsoft.Json;

namespace ShopView.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        Uri baseAddress = new Uri("https://localhost:44317/api/UserApi");
        HttpClient client;
        public UsersController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            
          }
        
        public ActionResult Index()
        {
            return View("Index","Home");
        }

        [HttpGet]
        public ActionResult Login(UserLogin userLogin)
        {
        
            return View(userLogin);
        }

        [HttpPost]
        public ActionResult Login1(UserLogin userLogin)
        {
            string data = JsonConvert.SerializeObject(userLogin);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/login", content).Result;

           if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<String>(temp);
                if (user == "ok")
                {
 
                    return RedirectToAction("Login", userLogin);
                }
                else
                {
                   
                    return RedirectToAction("Login");
                }
            }
            return HttpNotFound("UserLogin");
           
        }


        [HttpPost]
        public ActionResult Verify(UserLogin userLogin)
        {
            string data = JsonConvert.SerializeObject(userLogin);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(temp);
                Session["Userlogin"] = user.CustomerID;
                Session["Username"] = user.Name;
                return RedirectToAction("Index","Home", user);
            }
            return HttpNotFound();
           
        }

        [HttpGet]
        public ActionResult UserSingIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserSingIn(UserSignIn userSignIn)
        {
            
            string data = JsonConvert.SerializeObject(userSignIn);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Signin", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Index", "Home");
            }
            return HttpNotFound("UserLogin");
        }

        [HttpGet]
        public ActionResult Cart(int UserId)
        {
  //          string customer_id = Session["Userlogin"].ToString(); 
           // int number_customer_id= (int)Session["Userlogin"];
//            int.TryParse(customer_id, out number_customer_id);
            if (UserId != 0) { 
            
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/GetCart?content="+ UserId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var temp = response.Content.ReadAsStringAsync().Result;
                    var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CartwithProduct>>(temp);
                    if (cart != null)
                    {
                        return View(cart);
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    return RedirectToAction("UserLogin");
                }
            }
            return HttpNotFound("UserLogin");

        }

        [HttpPost]
        public ActionResult Cart1(int ProductId,int? UserId)
        {
            if (UserId != null)
            {
                var cart = new Cart() { ProductID = ProductId, CustomerID = UserId };
                string data = JsonConvert.SerializeObject(cart);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Cart", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var temp = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("Cart", "Users");
                }
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
            return View();
        }
    }
}