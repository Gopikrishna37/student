using DBcontext.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopViewWebAPI.Service.Product_Service
{
    interface P_Logics {
        dynamic Index();
        dynamic Search(string word);
    }
    public class ProductLogics:P_Logics
    {

        private readonly ShopzoneEntities _shopzoneEntities;

        public ProductLogics()
        {
            _shopzoneEntities = new ShopzoneEntities();
        }
        public dynamic Index()
        {
            try
            {
                var data = _shopzoneEntities.Products.Where(_ => _.IsDeleted == false).Select(_ =>
                new { name=_.Name,model=_.Model,img=_.ImageLink,price=_.Price,rating=_.Rating}).ToList();
               
                return data;

            }
            catch (Exception e)
            {
                return e;
            }
          
        }

        public dynamic Search(string word)
        {
            try
            {
                var data = (from pr in _shopzoneEntities.Products where(pr.IsDeleted == false && (pr.Name.Contains(word) || pr.Model.Contains(word)))
                           select new {  pr.Name,  pr.Model, pr.ImageLink, pr.Price, pr.Rating }).ToList();

                return data;

            }
            catch (Exception e)
            {
                return e;
            }

        }
    }
}