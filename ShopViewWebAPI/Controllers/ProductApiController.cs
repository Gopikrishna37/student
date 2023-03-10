using DBcontext.DBModels;
using ShopViewWebAPI.Service.Product_Service;
using System;
using System.Web.Http;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace ShopViewWebAPI.Controllers
{
    public class ProductApiController : ApiController
    {
        private readonly ShopzoneEntities _shopzoneEntities;
        public ProductApiController()
        {
            _shopzoneEntities = new ShopzoneEntities();
        }
        P_Logics p = new ProductLogics();
        [Route("api/ProductApi/Index")]
        [HttpGet]
        public dynamic Index()
        {
            try
            {
                var listofproduct = new List<Product>();
                string spName = "exec shopzone1 {0},{1}";
                //var Query = string.Format(spName, groupId, accountId);
                var temp = System.Configuration.ConfigurationManager.ConnectionStrings["ShopzoneEntities"];
                using (var sqlConn = new SqlConnection(temp.ToString()))
                {
                    SqlCommand command = new SqlCommand(spName, (SqlConnection)sqlConn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listofproduct.Add(new Product { CustomerID = (int)reader[0], ProductID = (int)reader[1] });
                        }
                        command.Dispose();
                    }
                }
               // var product = _shopzoneEntities.Products.SqlQuery<Product>("shopzone1").ToList();
                //var product = _shopzoneEntities.Products.SqlQuery(conn => conn.Query<List<Product>>("shopzone1", null, null, true, 300, System.Data.CommandType.StoredProcedure));
                return Json(listofproduct);
                // return data;
            }
            catch (Exception e)
            {
                return e;
            }

        }


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

        [Route("api/ProductApi/Buy")]
        [HttpGet]
        public dynamic Buy(Cart cart)
        {
            try
            {
                
                var result = p.Buy(cart);
                return Json(result);
                //return data;
            }
            catch (Exception e)
            {
                return e;
            }

        }


    }

}
