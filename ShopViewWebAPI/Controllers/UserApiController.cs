using DBcontext.DBModels;
using DBcontext.ServiceModels;
using ShopViewWebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopViewWebAPI.Controllers
{
    public class UserApiController : ApiController
    {
        IUser_Service U = new User_method();
        [Route("api/UserApi/Login")]
        [HttpPost]
        public dynamic Post(UserLogin userLogin)
        {

            try
            {

                if (userLogin != null)
                {

                   var result= U.UserLogin(userLogin);
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


        [Route("api/UserApi/Signin")]
        [HttpPost]
        public dynamic Post(UserSignIn userSignIn)
        {
            try
            {
                if (userSignIn != null)
                {
                    U.UserSignIn(userSignIn);
                    return userSignIn;
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


        [Route("api/UserApi/GetCart")]
        [HttpGet]
        public dynamic GetCart(int content)
        {
            try
            {
                if (content != 0)
                {
                   var result= U.GetCart(content);
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

        [Route("api/UserApi/Cart")]
    [HttpPost]
    public dynamic PostCart(Cart cart)
    {
        try
        {
            if (cart != null)
            {
                U.PostCart(cart);
                return "ok";
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