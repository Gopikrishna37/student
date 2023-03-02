using ShopViewWebAPI.Service.Product_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopViewWebAPI.Controllers
{
    public class ProductApiController : ApiController
    {

        P_Logics p = new ProductLogics();
        [HttpGet]
        public dynamic Get()
        {
            try
            {
                var result = p.Index();
                return Json(result);
                // return data;
            }
            catch(Exception e)
            {
                return e;
            }

        }

        [Route("api/ProductApi/search")]
        [HttpGet]
        public dynamic Get(string content)
        {
            try
            {
               // var data = "jgjgj";
                var result = p.Search(content);
                return Json(result);
                 //return data;
            }
            catch (Exception e)
            {
                return e;
            }

        }
    }
    public class Search 
    {
    public string word { get; set; }
    }
}
