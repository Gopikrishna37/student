using DBcontext.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace ShopView.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44317/api/ProductApi");
        HttpClient client;
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Index()
        {
            /*HttpResponseMessage response = client.GetAsync("ProductApi/Index").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var productsdetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DBcontext.DBModels.Product>>(data);
                
                return View();
            }*/
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}