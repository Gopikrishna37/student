using DBcontext.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopViewWebAPI.Service.Product_Service
{
    interface P_Logics {
        dynamic Index();
        dynamic Search(string word);
        dynamic Buy(Cart cart);
    
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
                new { name=_.Name,model=_.Model,imagelink=_.ImageLink,productID=_.ProductID,price=_.Price,rating=_.Rating}).ToList();
               
                return data;

            }
            catch (Exception e)
            {
                return e;
            }
          
        }

       

        public dynamic Buy(Cart cart)
        {
            try
            {
                var datas = (from product in _shopzoneEntities.Products
                            where product.ProductID == cart.ProductID 
                            join seller in _shopzoneEntities.Sellers on product.SellerID equals seller.SellerID
                            join user in _shopzoneEntities.Users on cart.CustomerID equals user.CustomerID
                            select new{Customer_name=user.Name, Customer_Email = user.Email,Product_Name=product.Name,Product_Model=product.Model,Product_Price=product.Price, Seller_Email = seller.Email, Seller_Name=seller.Name,seller}).FirstOrDefault();
                if (datas != null)
                {
                    Email mail = new Email();
                 //   string Customermailsent = mail.Sender(datas.Customer_Email, datas);
                   // string Sellermailsent = mail.Sender(datas.Customer_Email, datas);

                }
                return "fu";
            }
            catch(Exception e)
            {
                return e;
            }
        }
        

        public dynamic Search(string word)
        {
            try
            {
                var data = (from pr in _shopzoneEntities.Products where(pr.IsDeleted == false && (pr.Name.Contains(word) || pr.Model.Contains(word)))
                           select new { pr.ProductID, pr.Name,  pr.Model, pr.ImageLink, pr.Price, pr.Rating }).ToList();

                return data;

            }
            catch (Exception e)
            {
                return e;
            }

        }
    }
}