﻿using DBcontext.ServiceModels;
using ShopViewWebAPI.Service;
using ShopViewWebAPI.Views.SellerApi;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;

namespace ShopViewWebAPI.Controllers
{
    public class SellerApiController : ApiController
    {
        IsellerMethods m = new method();
        [Route("api/SellerApi/Signin")]
        [HttpPost]
        public dynamic Post(SellerSignIn sellerSignIn)
        {
            try
            {
                if (sellerSignIn != null)
                {
                  var Result=m.SellerSignIn(sellerSignIn);
                    return Result;
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception e)
            {
                return e;
            }
        }

        [Route("api/SellerApi/FileImport")]
        [HttpPost]
        public dynamic Post(List<Product> list)
        {
            try
            {
                if (list !=null)
                {
                    m.FileImport(list);
                    return "File Imported Successfully";
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception e)
            {
                return e;
            }
        }

        [HttpGet]
        public dynamic Get()
        {
            
                    
                    var result= m.SellerExisting();
                    return Json(result);
            // return data;


        }

        [HttpGet]
        public dynamic Get1(int id)
        {
            return "enjoy";
        }

      [Route ("api/SellerApi/login")]
        [HttpPost]
        public dynamic Post(SellerLogin sellerLogin)
        {
           
            try
            {
                
                if (sellerLogin != null)
                {
                    
                    var result=m.SellerLogin(sellerLogin);
                    return result;
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception e)
            {
                return e;
            }
        }


    }
}
