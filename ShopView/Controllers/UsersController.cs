using System;
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
 
                return RedirectToAction("Login", userLogin);
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
    }
}