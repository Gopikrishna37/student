using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using DBcontext.DBModels;
using DBcontext.ServiceModels;
using ShopViewWebAPI.Service;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using Seller = DBcontext.ServiceModels.Seller;

namespace ShopViewWebAPI.Views.SellerApi
{
    interface IsellerMethods
    {
        dynamic SellerSignIn(SellerSignIn sellerSignIn);
        dynamic SellerLogin(SellerLogin sellerLogin);
        dynamic SellerExisting();
        dynamic FileImport(List<DBcontext.ServiceModels.Product> file);
    }
    public class method : IsellerMethods
    {
        private readonly ShopzoneEntities _shopzoneEntities;

        public method()
        {
            _shopzoneEntities = new ShopzoneEntities();
        }

        public dynamic SellerSignIn(SellerSignIn sellerSignIn)
        {
            try
            {
                using (ShopzoneEntities db = new ShopzoneEntities())
                {
                    var seller = new DBcontext.DBModels.Seller()
                    {
                        Address = sellerSignIn.Address,
                        Name = sellerSignIn.Name,
                        City = sellerSignIn.City,
                        Zip = sellerSignIn.Zip,
                        Phone = sellerSignIn.Phone,
                        Email = sellerSignIn.Email,
                        CreatedOn = DateTime.Now
                    };
                    db.Sellers.Add(seller);
                    db.SaveChanges();
                    return "Success fully Added";
                }
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public dynamic SellerLogin(SellerLogin sellerLogin)
        {
            try
            {
                using (ShopzoneEntities db = new ShopzoneEntities())
                {
                    if (sellerLogin.OTP == null)
                    {

                        var temp = (from sel in db.Sellers where sel.Email == sellerLogin.Email select sel).FirstOrDefault();
                        if (temp != null)
                        {
                            Email mail = new Email();
                            Random generator = new Random();
                            int r = generator.Next(100000, 1000000);
                            temp.OTP = r.ToString();
                            string mailsent = mail.Sender(temp.Email, temp.OTP);
                            db.Entry(temp).State = EntityState.Modified;
                            db.SaveChanges();
                            return "ok";
                        }
                        else
                        {
                            return "Invalid User";
                        }
                    }

                    else
                    {
                        var check = (from sel in db.Sellers where sel.Email == sellerLogin.Email && sel.OTP == sellerLogin.OTP select sel).FirstOrDefault();
                        if (check != null)
                        {
                            var temp = new Seller()
                            {
                                Name=check.Name,
                                SellerID=check.SellerID
                            };
                            check.OTPSendTime = TimeSpan.Zero;
                            return temp;

                        }
                        return "otp is invalid";
                    }
                }
            }
            catch (Exception e)
            {
                return e;
            }
        }


        public virtual dynamic SellerExisting()
        {
            try
            {
                _shopzoneEntities.Configuration.ProxyCreationEnabled = false;
                _shopzoneEntities.Configuration.LazyLoadingEnabled = false;
                var data = _shopzoneEntities.Products.Where(_ => _.IsDeleted == false).Select(_ => _).ToList();
                //using (ShopzoneEntities db = new ShopzoneEntities())
                //{
                //    var Productdetails = (from val in db.Products where val.IsDeleted == false select val).ToList();
                //    return Productdetails;
                //}
                return data;

            }
            catch(Exception e)
            {
                return e;
            }
            
        }


        public dynamic FileImport(List<DBcontext.ServiceModels.Product> product)
        {
            try
            {
                using (ShopzoneEntities db = new ShopzoneEntities())
                {
                    // Create a new Student object.
                    DBcontext.DBModels.Product p = new DBcontext.DBModels.Product();
                    // Loop through each row of the DataTable.
                    foreach (var row in product)
                    {
                        // Set the properties of the Student object.
                        p.Name = row.Name;
                        p.Model = row.Model;
                        p.Price = row.Price;
                        p.Rating = row.Rating;
                        p.ImageLink = row.ImageLink;
                        p.IsDeleted = false;
                        p.SellerID = 1;
                        p.CreatedOn = DateTime.Now;
                        db.Products.Add(p);
                        db.SaveChanges();
                    }

                    return "File uploaded successfully";
                }
            }


            catch (Exception e)
            {
                return e;
            }
        }


    }
}
    
