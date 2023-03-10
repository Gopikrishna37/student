﻿using DBcontext.DBModels;
using DBcontext.ServiceModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopViewWebAPI.Service
{
    interface IUser_Service
    {
        dynamic UserSignIn(UserSignIn userSingIn);
        dynamic UserLogin(UserLogin userLogin);
        dynamic PostCart(Cart cart);
        dynamic GetCart(int id);
    }
    public class User_method:IUser_Service
    {
        private readonly ShopzoneEntities _shopzoneEntities;
        public User_method()
        {
            _shopzoneEntities = new ShopzoneEntities();
        }
        public dynamic UserSignIn(DBcontext.ServiceModels.UserSignIn userSignIn)
        {
            try
            {
                
                    var user= new DBcontext.DBModels.User()
                    {
                        Address = userSignIn.Address,
                        Name = userSignIn.Name,
                        City = userSignIn.City,
                        Zip = userSignIn.Zip,
                        Phone = userSignIn.Phone,
                        Email = userSignIn.Email,
                        CreatedOn = DateTime.Now
                    };
                    _shopzoneEntities.Users.Add(user);
                    _shopzoneEntities.SaveChanges();
                    return "Success fully Added";
                
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public dynamic UserLogin(DBcontext.ServiceModels.UserLogin userLogin)
        {
            try
            {
               
                    if (userLogin.OTP == null)
                    {

                        var temp = (from user in _shopzoneEntities.Users where user.Email == userLogin.Email select user).FirstOrDefault();
                        if (temp != null)
                        {
                            Email mail = new Email();
                            Random generator = new Random();
                            int r = generator.Next(100000, 1000000);
                            temp.OTP = r.ToString();
                            string mailsent = mail.Sender(temp.Email, temp.OTP);
                        _shopzoneEntities.Entry(temp).State = EntityState.Modified;
                        _shopzoneEntities.SaveChanges();
                            return "ok";
                        }
                        else
                        {
                            return "Invalid User";
                        }
                    }

                    else
                    {
                        var check = (from user in _shopzoneEntities.Users where user.Email == userLogin.Email && user.OTP == userLogin.OTP select user).FirstOrDefault();
                        if (check != null)
                        {
                        var loginperon = new User()
                        {
                            CustomerID=check.CustomerID,
                            Name=check.Name
                        };
                            check.OTPSendTime = TimeSpan.Zero;
                            return loginperon;

                        }
                        return "otp is invalid";
                    }
                
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public dynamic GetCart(int id)
        {
            try
            {
                _shopzoneEntities.Configuration.ProxyCreationEnabled = false;
                _shopzoneEntities.Configuration.LazyLoadingEnabled = false;
                var data = (from cart in _shopzoneEntities.Carts where cart.CustomerID==id  join pr in _shopzoneEntities.Products on cart.ProductID equals pr.ProductID select new 
                {
                    ProductID =pr.ProductID,
                    Name =pr.Name,
                    Model =pr.Model,
                    Price = pr.Price,
                    ImageLink = pr.ImageLink,
                    CartID = cart.CartID,
                    CustomerID = cart.CustomerID
                }).ToList();
                return data;

            }
            catch (Exception e)
            {
                return e;
            }

        }

        public dynamic PostCart(Cart cart)
        {
            try
            {
                cart.CreatedOn = DateTime.Now;
                cart.Total_Product = +1;
                var data = _shopzoneEntities.Carts.Add(cart);
                _shopzoneEntities.SaveChanges();
                return data;

            }
            catch (Exception e)
            {
                return e;
            }

        }
    }
}