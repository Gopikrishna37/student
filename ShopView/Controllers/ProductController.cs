using DBcontext.DBModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace ShopView.Controllers
{
    public class ProductController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44317/api/ProductApi");
        HttpClient client;
        public ProductController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {

            HttpResponseMessage response = client.GetAsync("ProductApi").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var productsdetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DBcontext.DBModels.Product>>(data);
                return View("AllProducts",productsdetails);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Search(string word)
        {
            
            //StringContent content= new StringContent(word, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.GetAsync(client.BaseAddress+"/search?content="+word).Result;


            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var productsdetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DBcontext.DBModels.Product>>(data);
                return View("AllProducts",productsdetails);
            }
            return View();
        }

        [HttpGet]
        public ActionResult AllProducts(IList<Product> item)
        {   
            
           return View(item);
        }

        [HttpPost]
        public ActionResult Buy(int ProductId,int UserId)
        {
            var cart = new Cart() { ProductID = ProductId, CustomerID = UserId };
            string data = JsonConvert.SerializeObject(cart);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Buy", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
     
    }
}